using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializedVector
{
    public float x, y, z;
    public Vector3 GetPos()
    {
        return new Vector3(x, y, z);
    }
    public SerializedVector(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }
}
[System.Serializable]
public class SerializedObject
{
    public float serializationTime;
}
[System.Serializable]
public class SerializedLevel: SerializedObject
{
    public float water;
}
[System.Serializable]
public class SerializedHelperPlant: SerializedObject
{
    public string type;
    public SerializedVector pos;
    public int spawnedResources;
    public float currentHarvestTimer;
}

public class HelperPlant : HPObject
{
    public AudioClip plantSound;
    public AudioClip removeSound;
    public string type;
    [HideInInspector]
    public int slot;
    public Collider2D plantCollider;
    public int spawnedResourceCount;
    int maxSpawnedResourceCount = 3;

    public Sprite iconSprite;

    public bool isWater = false;

    public bool ignorePest = false;

    public bool isDragging = true;

    public Transform resourcePositionsParent;
    int currentResourcePositionId;
    int resourcePositionCount;

    float harvestTime;
    float currentHarvestTimer;
    SerializedHelperPlant initData;

    public override SerializedObject Save()
    {
        var p= new SerializedHelperPlant();
        p.type = type;
        p.pos = new SerializedVector(transform.position);
        p.currentHarvestTimer = currentHarvestTimer;
        p.serializationTime = Time.time;
        p.spawnedResources = spawnedResourceCount;
        return p;
    }

    public void loadFromData(SerializedHelperPlant data)
    {
        initData = data;
        
    }

    void loadWithInitData()
    {
        if (initData == null)
        {
            return;
        }
        isDragging = false;
        float diffTime = Time.time - initData.serializationTime;
        int harvestCount = initData.spawnedResources;
        if (wouldHarvest())
        {
            var offlineHarvestCount = (int)Mathf.Floor(diffTime / getHarvestTime());
            harvestCount += offlineHarvestCount;
            harvestCount = Mathf.Min(harvestCount, maxSpawnedResourceCount);
            currentHarvestTimer = diffTime - (harvestCount - initData.spawnedResources) * getHarvestTime() + initData.currentHarvestTimer;

            for (int i = 0; i < harvestCount; i++)
            {
                if (!wouldHarvest())
                {
                    Debug.LogError("why would not harvest?");
                }
                Harvest();
            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (resourcePositionsParent)
        {
            resourcePositionCount = resourcePositionsParent.childCount;
            //maxSpawnedResourceCount = resourcePositionCount;

        }
        harvestTime = JsonManager.Instance.getPlant(type).harvestTime;
        loadWithInitData();
    }

    private void OnMouseEnter()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!isDragging)
        {

            HUD.Instance.ShowPlantDetail(gameObject);
            if (GetComponent<UnityChan.RandomWind>())
            {

                GetComponent<UnityChan.RandomWind>().interact();
            }
        }
    }

    IEnumerator delayAddCoin()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        CollectionManager.Instance.AddCoins(transform.position, JsonManager.Instance.getPlant(type).produces, false);
    }

    private void Plant()
    {

        audiosource.PlayOneShot(plantSound);
        isDragging = false;
        if (canPlant())
        {
            PlantsManager.Instance.startTreePlant(type);
            PlantsManager.Instance.Purchase(gameObject);
            StartCoroutine(delayAddCoin());
        }
        else
        {
            Destroy(gameObject);
        }

        //PlantsManager.Instance.shadowCollider.gameObject.SetActive(false);

    }

    private void OnMouseDown()
    {

       // GetComponent<UnityChan.RandomWind>().interact();
    }

    private void OnMouseExit()
    {
        if (!isDragging)
        {
            HUD.Instance.HidePlantDetail();
        }
    }

    protected void OnMouseOver()
    {
        if (!isDragging&& Input.GetMouseButtonDown(1))
        {
            RemovePlant();
        }
    }

    public void remove()
    {

        PlantsManager.Instance.audiosource.PlayOneShot(removeSound);
        die();
        HUD.Instance.HidePlantDetail();
    }
    protected virtual void RemovePlant()
    {
        Popup.Instance.Init(Dialogues.RemovePlantConfirm, remove);
    }

    public override void die()
    {
        var summons = GetComponent<SummonPlant>();
        if (summons)
        {
            summons.clean();
        }

        CollectionManager.Instance.RemoveCoins(transform.position, JsonManager.Instance.getPlant(type).produces, true);
        base.die();
    }
    bool canPlant()
    {
        return PlantsManager.Instance.IsPlantable(type, plantCollider,isWater);
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (isDragging)
        {
            if (!canPlant())
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
            if (Input.GetMouseButtonUp(0) && isDragging)
                Plant();

        }
        else
        {
            if (resourcePositionsParent && wouldHarvest() && currentHarvestTimer > getHarvestTime())
            {
                currentHarvestTimer = 0;
                Harvest();
            }
            currentHarvestTimer += Time.deltaTime;
        }
    }

    bool wouldHarvest()
    {
        return getHarvestTime() != float.MaxValue && spawnedResourceCount < maxSpawnedResourceCount;
    }

    

    float getHarvestTime()
    {
        if (harvestTime > 100)
        {
            return float.MaxValue;
        }
        return harvestTime;
    }

    void Harvest()
    {
        spawnedResourceCount++;
        Transform spawnTransform = resourcePositionsParent.GetChild(currentResourcePositionId);
        currentResourcePositionId++;
        if (currentResourcePositionId >= resourcePositionCount)
        {
            currentResourcePositionId = 0;
        }
        var go = Instantiate(PlantsManager.Instance.ClickToCollect, spawnTransform.position, Quaternion.identity,PlantsManager.Instance.resourceParent);
        ClickToCollect ctc = go.GetComponent<ClickToCollect>();
        ctc.parentPlant = this;
        var box = go.GetComponent<ClickToCollect>();
        box.dropboxType = DropboxType.resource;


        box.resource = JsonManager.Instance.getPlant(type).produces;
        box.UpdateImage();
    }

    public void resourceCollect()
    {
        spawnedResourceCount--;
    }

    public void MoveToGarden()
    {
        Destroy(this);
    }
}
