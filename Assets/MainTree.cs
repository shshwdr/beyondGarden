using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SerializedMainTree : CSSerializedObject
{
    public string type;
    public List<bool> isFlowerPositionSpawned;
    public List<bool> isFlowerPositionPollinated;
}
public class MainTree : HelperPlant
{
    public List<AudioClip> growSound;
    public AudioClip flowerSound;
    public AudioClip fruitSound;

    public Sprite background;

    public int fruitNumberToFinish;
    int fruitNumberFinished;
    public string flowerPlantType;
    public List<int> slotCount = new List<int>() { 2, 4, 6 };
    int currentLevel = 0;
    public GameObject treeFlowerPrefab;
    public Transform flowerPositionParent;
    List<Transform> flowerGeneratedPositions;
    List<bool> isFlowerPositionSpawned;
    List<bool> isFlowerPositionPollinated;
    int totalFlowerNumber;
    int currentFlowerNumber;

    public string finishType { get { return type; /*upgradeList[upgradeList.Count-1];*/ } }


    public SerializedMainTree serialize()
    {
        SerializedMainTree res = new SerializedMainTree();
        res.type = type;
        res.isFlowerPositionSpawned = isFlowerPositionSpawned;
        res.isFlowerPositionPollinated = isFlowerPositionPollinated;
        res.isValid = true;
        return res;
    }


    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
        loadWithInitData();

    }

    protected override void loadWithInitData()
    {

        PlantsManager.Instance.maintree = this;
        isDragging = false;


        flowerGeneratedPositions = new List<Transform>();

        foreach (Transform t in flowerPositionParent)
        {
            flowerGeneratedPositions.Add(t);
        }

        if (PlantsManager.Instance.serializedMainTree.isValid)
        {
            var sdata = PlantsManager.Instance.serializedMainTree;
            isFlowerPositionSpawned = sdata.isFlowerPositionSpawned;
            isFlowerPositionPollinated = sdata.isFlowerPositionPollinated;
            type = sdata.type;
            //show correct tree state
            switch (treeInfo.levelOfTree)
            {
                case 1:
                    GetComponent<Animator>().SetTrigger("grow");
                    break;

                case 2:
                    GetComponent<Animator>().SetTrigger("grow2");
                    break;
                case 3:
                    GetComponent<Animator>().SetTrigger("grow3");
                    break;

            }
            //add flowers and fruit
            for (int i = 0; i < isFlowerPositionSpawned.Count; i++)
            {
                if (isFlowerPositionSpawned[i])
                {

                    if (isFlowerPositionPollinated[i])
                    {
                        fruitNumberFinished++;
                        Instantiate(treeFlowerPrefab.GetComponent<TreeFlower>().fruitPrefab, flowerGeneratedPositions[i].position, Quaternion.identity, transform);
                    }
                    else
                    {

                        var go = justSpawnFlower(i);
                    }
                }
            }
        }
        else
        {
            isFlowerPositionSpawned = new List<bool>();
            isFlowerPositionPollinated = new List<bool>();
            foreach (Transform t in flowerPositionParent)
            {
                flowerGeneratedPositions.Add(t);
                isFlowerPositionSpawned.Add(false);
                isFlowerPositionPollinated.Add(false);
            }
        }

        totalFlowerNumber = flowerGeneratedPositions.Count;
    }
    public override void die()
    {
        isAlive = false;
        HUD.Instance.Gameover();
        
    }

    GameObject justSpawnFlower(int i)
    {
        var flowerId = treeInfo.flowerId;
        GameObject flowerPrefab = Resources.Load<GameObject>("trees/" + flowerId);
        GameObject go = Instantiate(flowerPrefab, flowerGeneratedPositions[i].position, flowerGeneratedPositions[i].rotation, transform);
        go.GetComponent<TreeFlower>().init(this, i);
        return go;
    }
    public void SpawnFlower()
    {
        audiosource.PlayOneShot(flowerSound);
        currentFlowerNumber++;

        for (int i = 0;i< flowerGeneratedPositions.Count; i++)
        {
            if (!isFlowerPositionSpawned[i])
            {
                var go = justSpawnFlower(i);

                var treeInfo = JsonManager.Instance.getTree(type);
                var flowerInfo = JsonManager.Instance.getFlower(treeInfo.flowerId);

                CollectionManager.Instance.RemoveCoins(go.transform.position, flowerInfo.plantCost);
                isFlowerPositionSpawned[i] = true;

                PlantsManager.Instance.addExpForFlowerWeapon(treeInfo.flowerId, flowerGeneratedPositions[i]);
                break;
            }
        }
    }

    public bool isFinished()
    {
        return fruitNumberFinished >= fruitNumberToFinish;
    }



    public void createFruit(int indexOnTree)
    {
        
        audiosource.PlayOneShot(fruitSound);
        fruitNumberFinished++;
        isFlowerPositionPollinated[indexOnTree] = true;
        if (fruitNumberFinished >= fruitNumberToFinish)
        {
            //finish a tree
            //HUD.Instance.showGardenButton();
            //TutorialManager.Instance.finishTree(type);
        }

    }

    public void Upgrade()
    {
        if (!upgradable())
        {
            return;
        }
        currentLevel += 1;
        
        audiosource.PlayOneShot(growSound[currentLevel]);
        type = treeInfo.nextLevel;
        //BirdManager.Instance.startTreePlant(type);
        PlantsManager.Instance.startTreePlant(type);
        CollectionManager.Instance.RemoveCoins(transform.position, treeInfo.plantCost);
        HUD.Instance.ShowPlantDetail(gameObject);
        GetComponent<Animator>().SetTrigger("grow");
    }

    public TreeInfo treeInfo { get { return JsonManager.Instance.getTree(type); } }

    public void PurchaseFlower()
    {
        if (canPurchaseFlower())
        {

            SpawnFlower();

            HUD.Instance.ShowPlantDetail(gameObject);
        }

    }


    public bool isAtMaxLevel()
    {
        return JsonManager.Instance.getTree(type).isMaxLevel;
    }

    public bool upgradable()
    {
        return PlantsManager.Instance.IsPlantable(nextLevelType());
    }

    public bool canPurchaseFlower()
    {
        var flowerId = JsonManager.Instance.getTree(type).flowerId;
        if(flowerId == null)
        {
            Debug.Log("flowerid does not exist for tree " + type);
        }
        return PlantsManager.Instance.IsPlantable(flowerId);
    }

    public bool isAtMaxFlower()
    {
        return currentFlowerNumber >= totalFlowerNumber;
    }
    void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (isAtMaxLevel())
        {

            if (!isAtMaxFlower())
            {
                PurchaseFlower();
            }
            return;
        }
        Upgrade();
    }

    public string nextLevelType()
    {
        return JsonManager.Instance.getTree(type).nextLevel;
    }

}
