using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToCollect : MonoBehaviour
{
    bool isClicked = false;
    public DropboxType dropboxType;
    public HelperPlant parentPlant;
    public List<PairInfo> resource;
    //public HelperPlantType unlockPlant;
    public float speed = 1f;
    public float amplitude = 1f;
    public bool needClick;
    // Start is called before the first frame update
    void Start()
    {
    }
    static public GameObject createClickToCollectItem(List<PairInfo> r, Transform t,bool needClick = true)
    {
        GameObject collectInBattlePrefab = Resources.Load<GameObject>("prefabs/clickToCollectBattle");
        var go = Instantiate(collectInBattlePrefab, t.position, Quaternion.identity, PlantsManager.Instance.resourceParent);
        go.transform.localScale = collectInBattlePrefab.transform.localScale;
        var box = go.GetComponent<ClickToCollect>();
        box.dropboxType = DropboxType.resource;
        box.resource = r;
        box.UpdateImage();
        box.needClick = needClick;
        return go;
    }
    public void UpdateImage()
    {

        string maxP = "";
        int maxV = 0;
        foreach (var pair in resource)
        {
            if (pair.Value > maxV)
            {
                maxV = pair.Value;
                maxP = pair.Key;
            }
        }
        GetComponent<SpriteRenderer>().sprite = JsonManager.Instance.getItemInfo(maxP).sprite;//  HUD.Instance.propertyImage[(int)(maxP));
    }

    private void OnMouseDown()
    {
        collect();
    }

    private void OnMouseOver()
    {
        
        if (!needClick )
        {
            collect();


        }
    }

    void collect()
    {
        if (parentPlant)
        {
            parentPlant.resourceCollect();
        }
        if (!isClicked)
        {
            isClicked = true;
        }
        if (dropboxType == DropboxType.unlock)
        {
            //PlantsManager.Instance.UnlockPlant(unlockPlant);
            //BirdManager.Instance.needToUnlock[unlockPlant] = false;
            //TutorialManager.Instance.firstSeeSomething("unlock");
            //CollectionManager.Instance.AddCoins(transform.position, resource);
        }
        else
        {
            if (GameManager.Instance.isInBattle)
            {
                Inventory.Instance.addItem(resource);
            }
            else
            {

                CollectionManager.Instance.AddCoins(transform.position, resource);
            }
        }
        Destroy(gameObject);
    }

    void Update()
    {
        var verticalMove = Mathf.Sin(Time.realtimeSinceStartup * speed) * amplitude * Vector3.up * Time.deltaTime;
        transform.position +=  verticalMove;
    }
}
