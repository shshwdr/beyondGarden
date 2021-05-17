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
public class SerializedObject { }
[System.Serializable]
public class SerializedHelperPlant: SerializedObject
{
    public SerializedVector pos;
    public float currentHarvestTimer;
}

public class HelperPlant : HPObject
{
    public AudioClip plantSound;
    public AudioClip removeSound;
    public HelperPlantType type;
    [HideInInspector]
    public int slot;
    public Collider2D plantCollider;

    public Sprite iconSprite;

    public bool isWater = false;

    public bool ignorePest = false;

    protected bool isDragging = true;

    public Transform resourcePositionsParent;
    int currentResourcePositionId;
    int resourcePositionCount;

    float harvestTime;
    float currentHarvestTimer;

    public override SerializedObject Save()
    {
        var p= new SerializedHelperPlant();
        p.pos = new SerializedVector(transform.position);
        p.currentHarvestTimer = currentHarvestTimer;
        return p;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (resourcePositionsParent)
        {
            resourcePositionCount = resourcePositionsParent.childCount;

        }
        harvestTime = PlantsManager.Instance.helperPlantProdTime.ContainsKey(type)? PlantsManager.Instance.helperPlantProdTime[type]:10000;
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
        CollectionManager.Instance.AddCoins(transform.position, PlantsManager.Instance.helperPlantProd[type], false);
    }

    private void Plant()
    {

        audiosource.PlayOneShot(plantSound);
        isDragging = false;
        if (canPlant())
        {
            PlantsManager.Instance.startTreePlant(type);
            PlantsManager.Instance.Purchase(gameObject);
            PlantsManager.Instance.AddPlant(this);
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
        CollectionManager.Instance.RemoveCoins(transform.position, PlantsManager.Instance.helperPlantProd[type],true);
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
            if (resourcePositionsParent && harvestTime<100 && currentHarvestTimer > harvestTime)
            {
                currentHarvestTimer = 0;
                Harvest();
            }
            currentHarvestTimer += Time.deltaTime;
        }
    }

    void Harvest()
    {
        Transform spawnTransform = resourcePositionsParent.GetChild(currentResourcePositionId);
        currentResourcePositionId++;
        if (currentResourcePositionId >= resourcePositionCount)
        {
            currentResourcePositionId = 0;
        }
        var go = Instantiate(PlantsManager.Instance.ClickToCollect, spawnTransform.position, Quaternion.identity,PlantsManager.Instance.resourceParent);
        var box = go.GetComponent<CllickToCollect>();
        box.dropboxType = DropboxType.resource;


        box.resource = PlantsManager.Instance.helperPlantProd[type];
        box.UpdateImage();
    }

    public void MoveToGarden()
    {
        Destroy(this);
    }
}
