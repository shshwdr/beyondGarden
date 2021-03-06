using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//public enum HelperPlantType { crimson, marigold, pond, lavender,
//    appleTree1, appleTree2, appleTree3, appleTree4, appleTreeFlower,
//    waterlily, lupin, zinnia, stawberry,
//    peachTree1, peachTree2, peachTree3, peachTree4, peachTreeFlower,
//    lemonTree1, lemonTree2, lemonTree3, lemonTree4, lemonTreeFlower,
//    cherryTree1, cherryTree2, cherryTree3, cherryTree4, cherryTreeFlower,
//    marsh, sweat, sun, viola, water , indigo, snapdragon, lutos, cone,
//};

public class UpgradeInfo
{
    public string id;
    public int level;
    public int nextLevel { get { return level + 1; } }
    public int exp;
    FlowerInfo flowerInfo()
    {
        return JsonManager.Instance.getFlower(id);
    }

    public void addExp(int e)
    {
        exp += e;
        int expNeeded = flowerInfo().expForLevel(nextLevel);
        if (exp >= expNeeded)
        {
            level += 1;
        }
    }
    public UpgradeInfo(string i,int l,int e)
    {
        id = i;
        level = l;
        exp = e;
    }
}
public class PlantsManager : Singleton<PlantsManager>
{
    public  AudioSource audiosource;
    public SpriteRenderer background;
    public bool ignoreResourcePlant = true;
    public bool unlockAllFlowers = true;
    public MainTree maintree;
    public GameObject mainTreePrefab;
    public Dictionary<string, bool>  isPlantUnlocked =  new Dictionary<string, bool>();
    public Dictionary<string, UpgradeInfo> plantUpgradeStatusDict = new Dictionary<string, UpgradeInfo>();
    [HideInInspector]
    public List<SerializedHelperPlant> serializedPlantedPlant = new List<SerializedHelperPlant>();
    public SerializedMainTree serializedMainTree;

    public override CSSerializedObject Save()
    {
        var res = new SerializedLevel();
        res.water = PlantsManager.Instance.currentResource["water"];
        return res;
    }


    //Dictionary<HelperPlantType, HelperPlantType> treeToUnlockFlower = new Dictionary<HelperPlantType, HelperPlantType>()
    //{
    //    {HelperPlantType.pond, HelperPlantType.waterlily },
    //    {HelperPlantType.waterlily, HelperPlantType.crimson },
    //    {HelperPlantType.crimson, HelperPlantType.lavender },
    //    {HelperPlantType.lavender, HelperPlantType.marigold },
    //};

    public Dictionary<string, List<string>> levelToPlants = new Dictionary<string, List<string>>()
    {

        //{HelperPlantType.appleTree1,
        //    new List<HelperPlantType>(){ HelperPlantType.crimson, HelperPlantType.marigold, HelperPlantType.waterlily, HelperPlantType.lavender, HelperPlantType.zinnia, HelperPlantType.stawberry, HelperPlantType.pond, } },

        //{HelperPlantType.lemonTree1,
        //    new List<HelperPlantType>(){ HelperPlantType.marsh, HelperPlantType.marigold, HelperPlantType.sweat, HelperPlantType.lavender, HelperPlantType.sun, HelperPlantType.viola, HelperPlantType.pond, } },

        //{HelperPlantType.peachTree1,
        //    new List<HelperPlantType>(){ HelperPlantType.water, HelperPlantType.indigo, HelperPlantType.lupin, HelperPlantType.viola, HelperPlantType.zinnia, HelperPlantType.stawberry, HelperPlantType.pond, } },

        //{HelperPlantType.cherryTree1,
        //    new List<HelperPlantType>(){ HelperPlantType.lupin, HelperPlantType.lutos, HelperPlantType.sun, HelperPlantType.snapdragon, HelperPlantType.cone, HelperPlantType.indigo, HelperPlantType.pond, } },
    };


    public GameObject ClickToCollect;

    public Collider2D groundCollider2;
    public Collider2D groundCollider1;

    public Transform resourceParent;


    //public Dictionary<HelperPlantType, string> levelDetail;


    public Dictionary<string, bool> isResourceUnlocked = new Dictionary<string, bool>();
    public Dictionary<string, GameObject> helperPlantDict = new Dictionary<string, GameObject>();

    float currentTime = 0;

    public Dictionary<string, int>  currentResource = new Dictionary<string, int>();


    public void gotoDungeon()
    {
        MainGameManager.Instance. serializeData();
        Utils.setChildrenToInactive(resourceParent);
        GameManager.Instance.getIntoBattle();
    }

    void initValues()
    {
        //    levelDetail = new Dictionary<HelperPlantType, string>() {
        //    { HelperPlantType.appleTree1,"This is a simple tutorial level." },
        //    { HelperPlantType.peachTree1,"This level has more request on P." },
        //    { HelperPlantType.lemonTree1,"This level has more request on N." },
        //    { HelperPlantType.cherryTree1,"This is level has more request on all elements" },
        //    };


        //    plantName = new Dictionary<HelperPlantType, string>() {


        //    { HelperPlantType.appleTree1, "Apple Tree - Sprout" },
        //    { HelperPlantType.appleTree2, "Apple Tree - Sapling" },
        //    { HelperPlantType.appleTree3, "Apple Tree - Juvenile" },
        //    { HelperPlantType.appleTree4, "Apple Tree - Adult" },
        //    { HelperPlantType.appleTreeFlower, "Apple Tree - flower" },


        //    { HelperPlantType.peachTree1, "Peach Tree - Sprout" },
        //    { HelperPlantType.peachTree2, "Peach Tree - Sapling" },
        //    { HelperPlantType.peachTree3, "Peach Tree - Juvenile" },
        //    { HelperPlantType.peachTree4, "Peach Tree - Adult" },
        //    { HelperPlantType.peachTreeFlower, "Peach Tree - flower" },

        //    { HelperPlantType.lemonTree1, "Lemon Tree - Sprout" },
        //    { HelperPlantType.lemonTree2, "Lemon Tree - Sapling" },
        //    { HelperPlantType.lemonTree3, "Lemon Tree - Juvenile" },
        //    { HelperPlantType.lemonTree4, "Lemon Tree - Adult" },
        //    { HelperPlantType.lemonTreeFlower, "Lemon Tree - flower" },

        //    { HelperPlantType.cherryTree1, "Cherry Tree - Sprout" },
        //    { HelperPlantType.cherryTree2, "Cherry Tree - Sapling" },
        //    { HelperPlantType.cherryTree3, "Cherry Tree - Juvenile" },
        //    { HelperPlantType.cherryTree4, "Cherry Tree - Adult" },
        //    { HelperPlantType.cherryTreeFlower, "Cherry Tree - flower" },


        //    { HelperPlantType.marsh, "Marsh Marigold" },
        //    { HelperPlantType.sweat, "Sweet pea" },
        //    { HelperPlantType.sun, "Sunflower" },
        //    { HelperPlantType.viola, "Viola" },

        //    { HelperPlantType.cone, "ConeFlower" },

        //    { HelperPlantType.water, "Water Hyacinth" },
        //    { HelperPlantType.indigo, "False indigo" },
        //    { HelperPlantType.snapdragon, "Snapdragon" },
        //    { HelperPlantType.lutos, "Lotus" },

        //};
        //helperPlantProdTime = new Dictionary<HelperPlantType, float>() {
        //    //N
        //    {HelperPlantType.sweat, 12},
        //    {HelperPlantType.indigo, 9},
        //    {HelperPlantType.lupin, 15},

        //    //P
        //    {HelperPlantType.sun, 12},
        //    {HelperPlantType.cone, 15},

        //    {HelperPlantType.pond, 10},

        //    //bee
        //    {HelperPlantType.marigold, 100000},
        //    {HelperPlantType.zinnia, 100000},
        //    {HelperPlantType.viola, 100000},
        //    {HelperPlantType.snapdragon, 100000},

        //};

        //    helperPlantProd = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
        //{
        //        //n

        //    {HelperPlantType.sweat,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.n, 6 },
        //        { PlantProperty.pest, 3 },
        //    }},
        //    {HelperPlantType.indigo,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.n, 3 },
        //        { PlantProperty.pest, 2 },
        //    }},

        //    //p
        //    {HelperPlantType.sun,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.p, 3 },
        //        { PlantProperty.n, 1 },
        //        { PlantProperty.pest, 2 },
        //    } },
        //        {HelperPlantType.cone,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.p, 8 },
        //        { PlantProperty.n, 1 },
        //        { PlantProperty.pest, 4 },
        //    }},

        //        //bee

        //    {HelperPlantType.viola,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.bee, 2 },
        //        { PlantProperty.pest, 3 },
        //    } },

        //    {HelperPlantType.snapdragon,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.bee, 3 },
        //        { PlantProperty.pest, 4 },
        //    } },


        //    {HelperPlantType.pond,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.water, 10 },
        //    } },
        //    //frog
        //    {HelperPlantType.waterlily,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.frog, 1 },
        //    } },
        //    {HelperPlantType.water,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.frog, 1 },
        //    } },
        //    {HelperPlantType.marsh,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.frog, 1 },
        //    } },
        //    {HelperPlantType.lutos,new Dictionary<PlantProperty, int>() {
        //        { PlantProperty.frog, 1 },{ PlantProperty.bee, 1 },
        //    } },
        //    {HelperPlantType.appleTree1,new Dictionary<PlantProperty, int>() {  } },
        //    {HelperPlantType.appleTree2,new Dictionary<PlantProperty, int>() { } },
        //    {HelperPlantType.appleTree3,new Dictionary<PlantProperty, int>() { } },
        //    {HelperPlantType.appleTree4,new Dictionary<PlantProperty, int>() { } },
        //}; 



        //    helperPlantCost = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
        //{

        //    {HelperPlantType.crimson,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 40 } } },
        //    {HelperPlantType.stawberry,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.n, 20 } } },
        //    {HelperPlantType.zinnia,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 50 }, { PlantProperty.n, 24 }, { PlantProperty.p, 30 } } },
        //    {HelperPlantType.lavender,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 16 } ,{ PlantProperty.water, 50 } } },
        //    {HelperPlantType.marigold,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 80 }, { PlantProperty.n, 20 }, { PlantProperty.p, 20 } } },

        //    {HelperPlantType.appleTree1,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,20}, { PlantProperty.p, 15 }  } },
        //    {HelperPlantType.appleTree2,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,50}, { PlantProperty.p, 30 } } },
        //    {HelperPlantType.appleTree3,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 100 }, { PlantProperty.p, 80 } } },
        //    {HelperPlantType.appleTree4,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 150 }, { PlantProperty.p, 100 } } },
        //    {HelperPlantType.appleTreeFlower,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 40 }, { PlantProperty.p, 40 } } },

        //    {HelperPlantType.peachTree1,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,20}, { PlantProperty.p, 50 }  } },
        //    {HelperPlantType.peachTree2,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,50}, { PlantProperty.p, 80 } } },
        //    {HelperPlantType.peachTree3,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 100 }, { PlantProperty.p, 120 } } },
        //    {HelperPlantType.peachTree4,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 150 }, { PlantProperty.p, 180 } } },
        //    {HelperPlantType.peachTreeFlower,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 40 }, { PlantProperty.p, 50 } } },


        //    {HelperPlantType.lemonTree1,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,40}, { PlantProperty.p, 15 }  } },
        //    {HelperPlantType.lemonTree2,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,90}, { PlantProperty.p, 30 } } },
        //    {HelperPlantType.lemonTree3,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 160 }, { PlantProperty.p, 80 } } },
        //    {HelperPlantType.lemonTree4,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 220 }, { PlantProperty.p, 100 } } },
        //    {HelperPlantType.lemonTreeFlower,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 80 }, { PlantProperty.p, 40 } } },

        //    {HelperPlantType.cherryTree1,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,40}, { PlantProperty.p, 30 }  } },
        //    {HelperPlantType.cherryTree2,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,90}, { PlantProperty.p, 60 } } },
        //    {HelperPlantType.cherryTree3,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 160 }, { PlantProperty.p, 100 } } },
        //    {HelperPlantType.cherryTree4,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 220 }, { PlantProperty.p, 180 } } },
        //    {HelperPlantType.cherryTreeFlower,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 80 }, { PlantProperty.p, 80 } } },


        //    {HelperPlantType.marsh,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 50 } } },
        //    {HelperPlantType.sweat,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 50 } } },
        //    {HelperPlantType.sun,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.n, 60 } } },
        //    {HelperPlantType.viola,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 50 }, { PlantProperty.n, 24 }, { PlantProperty.p, 30 } } },


        //    {HelperPlantType.water,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 50 } } },
        //    {HelperPlantType.indigo,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 50 } } },
        //    {HelperPlantType.lupin,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.n, 60 },{ PlantProperty.p, 30 } } },


        //    {HelperPlantType.cone,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.n, 80 },{ PlantProperty.p, 20 } } },
        //    {HelperPlantType.snapdragon,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 100 }, { PlantProperty.n, 60 },{ PlantProperty.p, 30 } } },
        //    {HelperPlantType.lutos,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 200 }, { PlantProperty.n, 60 }, { PlantProperty.p, 30 } } },


        //};


    }
    // Start is called before the first frame update
    private void Awake()
    {
        initValues();
        audiosource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        //foreach (var plant in helperPlantList)
        //{
        //    HelperPlant pScript = plant.GetComponent<HelperPlant>();
        //    helperPlantDict[pScript.type] = plant;
        //}
    }
    void Start()
    {
        foreach (var type in JsonManager.Instance.flowerDict.Keys)
        {
            if (!JsonManager.Instance.flowerDict[type].isTreeFlower)
            {

                GameObject plant = Resources.Load("flowers/" + type) as GameObject;
                if (plant)
                {

                    plant.GetComponent<HelperPlant>().type = type;
                    helperPlantDict[type] = plant;
                }
                else
                {
                    Debug.LogError("plant prefab does not existed " + type);
                }
            }
        }
        foreach (var pair in JsonManager.Instance.currencyDict)
        {
            currentResource[pair.Key] = pair.Value.initialValue;
        }


        //UpdateRate();
        ClearResource();
    }

    public void startTreePlant(string plantId)
    {
        //if(treeToUnlockFlower.ContainsKey(type))
        //{
        //    UnlockPlant(treeToUnlockFlower[type]);
        //}
        //if (helperPlantProd.ContainsKey(type))
        //{
        if (JsonManager.Instance.isFlower(plantId))
        {
            foreach (var product in JsonManager.Instance.getFlower(plantId).produces)
            {
                UnlockResource(product.Key);
            }
        }
        //}
        //TutorialManager.Instance.finishPlant(type);
    }

    public bool IsResourceAvailable(string resourceId, int value)
    {
        return currentResource[resourceId] >= value;
    }

    bool IsPositionValid(Collider2D col, bool isWaterPlant = false)
    {
        Collider2D[] colliders = new Collider2D[20];
        ContactFilter2D contactFilter = new ContactFilter2D();
        col.OverlapCollider(contactFilter, colliders);
        bool collideGround = true;
        bool collideOtherPlant = false;
        bool colliderWater = !isWaterPlant;
        foreach(var collided in colliders)
        {
            if (!collided)
            {
                break;
            }
            //Debug.Log(collided);
            if (collided == groundCollider1 || collided == groundCollider2)
            {
                collideGround = false;
                //break;
            }
            if(collided.name == "plant" && collided.GetComponentInParent<HelperPlant>())
            {
                if (isWaterPlant)
                {
                    if (collided.GetComponentInParent<HelperPlant>().type == "pond")
                    {
                        continue;
                    }
                }
                collideOtherPlant = true;
               // break;
            }
            if(collided.name == "pondwater")
            {
                colliderWater = true;
            }
        }

        
        return collideGround&& !collideOtherPlant && colliderWater;
    }

    public bool IsPlantable(string type)
    {
        if (!ignoreResourcePlant)
        {
            var prodDictionary = JsonManager.Instance.getPlant(type).plantCost;
            if (prodDictionary != null)
            {

                foreach (var pair in prodDictionary)
                {
                    if (currentResource[pair.Key] < pair.Value)
                    {
                        return false;
                    }
                }
            }

            //has seed
            if (!Inventory.Instance.hasEnoughSeed(type))
            {
                return false;
            }
        }
        return true;
    }
    public bool IsPlantable(string type, Collider2D pos, bool isWaterPlant = false)
    {
        return IsPlantable(type) && IsPositionValid(pos, isWaterPlant);
    }


    public void Purchase(GameObject plant)
    {
        var type = plant.GetComponent<HelperPlant>().type; 
        {
            CollectionManager.Instance.RemoveCoins(plant.transform.position, JsonManager.Instance.getPlant(type).plantCost);
            //ReduceCostForType(plant.GetComponent<HelperPlant>().type);
            MainGameManager.Instance. plantedPlant.Add(plant.GetComponent<HelperPlant>());

            Inventory.Instance.useSeed(type);

            if(type != "pond")
            {

                addExpForFlowerWeapon(type, plant.transform);
            }
        }
    }

    public void addExpForFlowerWeapon(string type, Transform textTransform)
    {

        var weaponInfo = JsonManager.Instance.getFlower(type);
        if (plantUpgradeStatusDict.ContainsKey(type))
        {
            plantUpgradeStatusDict[type].addExp(1);
            PopupTextManager.Instance.ShowPopupString(textTransform.position, weaponInfo.name + " exp +1", 3);
        }
        else
        {

            PopupTextManager.Instance.ShowPopupString(textTransform.position, "unlock weapon " + weaponInfo.name, 3);
            plantUpgradeStatusDict[type] = new UpgradeInfo(type, 1, 0);
        }
    }

    public void ReduceCostForType(string type)
    {
        //ReduceResource(currentResource, helperPlantCost[type]);
        foreach (var pair in JsonManager.Instance.getPlant(type).plantCost)
        {
            currentResource[pair.Key] -= pair.Value;
        }
    }

   
    public void ClearResource()
    {
        //foreach (var key in baseResource.Keys.ToArray<PlantProperty>())
        //{
        //    currentResource[key] = baseResource[key];
        //}
        if(resourceParent== null)
        {
            return;
        }
        foreach(Transform child in resourceParent)
        {
            child.gameObject.SetActive(false);
        }
        
    }

    //public void AddResource(Dictionary<PlantProperty, int> resource)
    //{
    //    foreach (var pair in resource)
    //        currentResource[pair.Key] += pair.Value;

    //}

    //public void UnlockPlant(HelperPlantType type)
    //{
    //    isPlantUnlocked[type] = true;
    //    HUD.Instance.UpdatePlantButtons();
    //}
    public void UnlockResource(string type)
    {
        isResourceUnlocked[type] = true;
    }

    public bool isIncreasingResource(string p)
    {
        return !(p == "bee" || p == "pest" || p == "frog");
    }
}
