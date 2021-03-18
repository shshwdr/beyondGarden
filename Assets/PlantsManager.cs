﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum PlantProperty { s, p, n, water, bee, pest, frog };
public enum HelperPlantType { red, yellow, blue, purple, appleTree1, appleTree2, appleTree3, appleTree4, appleTreeFlower, waterlily, lupin, zinnia, stawberry };
public class PlantsManager : Singleton<PlantsManager>
{
    public bool ignoreResourcePlant = true;
    public MainTree maintree;
    public GameObject mainTreePrefab;
    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantCost;
    //public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantKeepCost;
    public Dictionary<HelperPlantType, Dictionary<PlantProperty, int>> helperPlantProd;

    public Dictionary<HelperPlantType, float> helperPlantProdTime;

    Dictionary<HelperPlantType, HelperPlantType> treeToUnlockFlower = new Dictionary<HelperPlantType, HelperPlantType>()
    {
        {HelperPlantType.blue, HelperPlantType.waterlily },
        {HelperPlantType.waterlily, HelperPlantType.red },
        {HelperPlantType.red, HelperPlantType.purple },
        {HelperPlantType.purple, HelperPlantType.yellow },
    };

    //Dictionary<HelperPlantType, PlantProperty> treeToUnlockResource = new Dictionary<HelperPlantType, PlantProperty>()
    //{
    //    {HelperPlantType.blue, PlantProperty.water },
    //    {HelperPlantType.waterlily, PlantProperty.frog },
    //    {HelperPlantType.red, PlantProperty.n },
    //    {HelperPlantType.purple, PlantProperty.p },
    //    {HelperPlantType.yellow, PlantProperty.bee },
    //    {HelperPlantType.purple, PlantProperty.p },
    //};

    public Dictionary<PlantProperty, int> currentResource;
    public Dictionary<PlantProperty, int> baseResource;

    public Dictionary<PlantProperty, string> resourceName;

    public Dictionary<HelperPlantType, string> plantName;

    public GameObject ClickToCollect;

    public Collider2D groundCollider2;
    public Collider2D groundCollider1;
    public Collider2D shadowCollider;

    int currentShadowSizeId = 0;
    float[] shadowColliderSize = new float[] { 30, 37, 43, 50 };

    public Transform plantsSlotParent;
    List<PlantSlot> plantSlots;

    public int unlockedSlot = 2;


    public Dictionary<HelperPlantType, bool> isPlantUnlocked; 
    public Dictionary<PlantProperty, bool> isResourceUnlocked = new Dictionary<PlantProperty, bool>();
    public List<GameObject> helperPlantList;

    List<HelperPlant> plantedPlant = new List<HelperPlant>();
    float currentTime = 0;

    public Transform allInTreeGame;
    public List<Transform> plantsList()
    {
        List<Transform> res = new List<Transform>();
        foreach(var plantValue in plantedPlant)
        {
            if(plantValue && plantValue.isAlive)
            {
                res.Add(plantValue.transform);

            }
        }
        if (maintree && maintree.isAlive)
        {
            res.Add(maintree.transform);
        }
        return res;
    }
    void initValues()
    {
        resourceName = new Dictionary<PlantProperty, string>() {
        { PlantProperty.s, "K" },
        { PlantProperty.p, "P"  },
        { PlantProperty.n, "N" },
        { PlantProperty.water, "Water" },
        { PlantProperty.bee, "Bee Attrack" },
        { PlantProperty.pest, "Pest Attrack" },
        { PlantProperty.frog, "Frog Count" },
    };
        baseResource = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 0 },
        { PlantProperty.s, 0 },
        { PlantProperty.n, 0 },
        { PlantProperty.water, 40 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },
        { PlantProperty.frog, 0 },
    };


        isPlantUnlocked = new Dictionary<HelperPlantType, bool>() {
        { HelperPlantType.purple,false },
        { HelperPlantType.waterlily,false },
        { HelperPlantType.red,false },
        { HelperPlantType.yellow,false },
        { HelperPlantType.lupin,false },
        { HelperPlantType.zinnia,false },
        { HelperPlantType.stawberry,false },
        };
        plantName = new Dictionary<HelperPlantType, string>() {
        { HelperPlantType.red, "Crimon Clover" },
        { HelperPlantType.yellow, "Marigold" },
        { HelperPlantType.blue, "Pond" },
        { HelperPlantType.purple, "Lavender" },
        { HelperPlantType.appleTree1, "Apple Tree - Sprout" },
        { HelperPlantType.appleTree2, "Apple Tree - Sapling" },
        { HelperPlantType.appleTree3, "Apple Tree - Juvenile" },
        { HelperPlantType.appleTree4, "Apple Tree - Adult" },
        { HelperPlantType.appleTreeFlower, "Apple Tree - flower" },

        { HelperPlantType.waterlily, "Water Lily" },
        { HelperPlantType.lupin, "Lupin" },
        { HelperPlantType.zinnia, "Zinnia" },
        { HelperPlantType.stawberry, "Stawberry" },

    };
        helperPlantProdTime = new Dictionary<HelperPlantType, float>() {
            {HelperPlantType.red, 8},
            {HelperPlantType.purple, 9},
            {HelperPlantType.blue, 10},
            {HelperPlantType.waterlily, 100000},
            {HelperPlantType.yellow, 100000},
        };

        helperPlantProd = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() {
            { PlantProperty.n, 8 },
            { PlantProperty.pest, 2 },
        }},
        {HelperPlantType.purple,new Dictionary<PlantProperty, int>() {
            { PlantProperty.p, 6 },
            { PlantProperty.pest, 2 },
        } },
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() {
            { PlantProperty.bee, 2 },
            { PlantProperty.pest, 2 },
        } },
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() {
            { PlantProperty.water, 10 },
        } },
        {HelperPlantType.waterlily,new Dictionary<PlantProperty, int>() {
            { PlantProperty.frog, 1 },
        } },
        {HelperPlantType.appleTree1,new Dictionary<PlantProperty, int>() {  } },
        {HelperPlantType.appleTree2,new Dictionary<PlantProperty, int>() { } },
        {HelperPlantType.appleTree3,new Dictionary<PlantProperty, int>() { } },
        {HelperPlantType.appleTree4,new Dictionary<PlantProperty, int>() { } },
    }; 
        helperPlantCost = new Dictionary<HelperPlantType, Dictionary<PlantProperty, int>>()
    {
        {HelperPlantType.blue,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 0 } } },
        {HelperPlantType.waterlily,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 50 } } },
        {HelperPlantType.red,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 40 } } },
        {HelperPlantType.purple,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 16 } ,{ PlantProperty.water, 20 } } },
        {HelperPlantType.yellow,new Dictionary<PlantProperty, int>() { { PlantProperty.water, 20 }, { PlantProperty.n, 24 }, { PlantProperty.p, 20 } } },
        {HelperPlantType.appleTree1,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,20}, { PlantProperty.p, 15 }  } },
        {HelperPlantType.appleTree2,new Dictionary<PlantProperty, int>() {  { PlantProperty.n,40}, { PlantProperty.p, 25 } } },
        {HelperPlantType.appleTree3,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 80 }, { PlantProperty.p, 60 } } },
        {HelperPlantType.appleTree4,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 100 }, { PlantProperty.p, 80 } } },
        {HelperPlantType.appleTreeFlower,new Dictionary<PlantProperty, int>() { { PlantProperty.n, 40 }, { PlantProperty.p, 40 } } },
    };


        currentResource = new Dictionary<PlantProperty, int>() {
        { PlantProperty.p, 0 },
        { PlantProperty.s, 0 },
        { PlantProperty.n, 0 },
        { PlantProperty.water, 0 },
        { PlantProperty.bee, 0 },
        { PlantProperty.pest, 0 },
        { PlantProperty.frog, 0 },
    };
    }
    // Start is called before the first frame update
    private void Awake()
    {
        initValues();
        plantSlots = new List<PlantSlot>();
        for (int i = 0; i < plantsSlotParent.childCount; i++)
        {
            plantSlots.Add(plantsSlotParent.GetChild(i).GetComponent<PlantSlot>());
        }
    }
    void Start()
    {
        //UpdateRate();
        ClearResource();
    }

    public int firstAvailableSlot()
    {
        for (int i = 0; i < unlockedSlot; i++)
        {
            var slot = plantSlots[i];
            if (slot.isAvailable)
            {
                return i;
            }
        }
        return -1;
    }
    public void startTreePlant(HelperPlantType type)
    {
        if(treeToUnlockFlower.ContainsKey(type))
        {
            UnlockPlant(treeToUnlockFlower[type]);
        }

        foreach(var product in helperPlantProd[type].Keys)
        {
            UnlockResource(product);
        }
        TutorialManager.Instance.finishPlant(type);
    }
    public bool hasSlot()
    {
        return firstAvailableSlot() != -1;
    }
    public bool IsResourceAvailable(PlantProperty property, int value)
    {
        return currentResource[property] >= value;
    }

    [System.Obsolete("Method is obsolete.", false)]
    public bool IsPlantable(HelperPlantType type, bool ignoreSlot = false)
    {
        if (!ignoreResourcePlant)
        {
            var prodDictionary = helperPlantCost[type];
            foreach (var pair in prodDictionary)
            {
                if (currentResource[pair.Key] < pair.Value)
                {
                    return false;
                }
            }
        }
        return ignoreSlot || hasSlot();
    }
    bool IsPositionValid(Collider2D col, bool isWaterPlant = false)
    {
        if (!shadowCollider.OverlapPoint(col.transform.position))
        {
            return false;
        }
        Collider2D[] colliders = new Collider2D[20];
        ContactFilter2D contactFilter = new ContactFilter2D();
        col.OverlapCollider(contactFilter, colliders);
        bool collideGround = true;
        bool collideShadow = false;
        bool collideOtherPlant = false;
        bool colliderWater = !isWaterPlant;
        foreach(var collided in colliders)
        {
            if (!collided)
            {
                break;
            }
            if (collided == groundCollider1 || collided == groundCollider2)
            {
                collideGround = false;
                //break;
            }
            if (collided == shadowCollider)
            {
                collideShadow = true;
            }
            if(collided.name == "plant" && collided.GetComponentInParent<HelperPlant>())
            {
                if (isWaterPlant)
                {
                    if (collided.GetComponentInParent<HelperPlant>().type == HelperPlantType.blue)
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



        return collideGround&& collideShadow && !collideOtherPlant && colliderWater;
    }
    public bool IsPlantable(HelperPlantType type, Collider2D pos, bool isWaterPlant = false)
    {
        if (!ignoreResourcePlant)
        {
            var prodDictionary = helperPlantCost[type];
            foreach (var pair in prodDictionary)
            {
                if (currentResource[pair.Key] < pair.Value)
                {
                    return false;
                }
            }
        }
        return IsPositionValid(pos, isWaterPlant);
    }

    void ReduceResource(Dictionary<PlantProperty, int> origin, Dictionary<PlantProperty, int> reduce)
    {
        foreach (var pair in reduce)
        {
            origin[pair.Key] -= pair.Value;
        }
       // CollectionManager.Instance.RemoveCoins()
    }

    public void Purchase(GameObject plant)
    {
        //var slotId = firstAvailableSlot();
       // if (slotId != -1)
        {

            //var slot = plantSlots[slotId];
            CollectionManager.Instance.RemoveCoins(plant.transform.position, PlantsManager.Instance.helperPlantCost[plant.GetComponent<HelperPlant>().type]);
            //ReduceCostForType(plant.GetComponent<HelperPlant>().type);
            plantedPlant.Add(plant.GetComponent<HelperPlant>());
            //GameObject spawnInstance = Instantiate(plantPrefab, slot.transform);
            //slot.isAvailable = false;
            //plantedPlant[slotId] = spawnInstance.GetComponent<HelperPlant>();
            //spawnInstance.GetComponent<HelperPlant>().slot = slotId;
        }
    }

    public void ReduceCostForType(HelperPlantType type)
    {
        ReduceResource(currentResource, helperPlantCost[type]);

       // PlantsManager.Instance.UpdateRate();
    }

    

    public void AddPlant(HelperPlant newPlant)
    {
        //UpdateRate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increaseShadowSize()
    {
        currentShadowSizeId++;
        if(currentShadowSizeId >= shadowColliderSize.Length)
        {
            currentShadowSizeId = shadowColliderSize.Length - 1;
        }
        UpdateShadow();
    }
    void UpdateShadow()
    {
        var size = shadowColliderSize[currentShadowSizeId];
        shadowCollider.gameObject.transform.localScale = new Vector3(size, size, 1);
    }
    public void ClearResource()
    {
        currentShadowSizeId = 0;
        UpdateShadow();
        foreach (var key in baseResource.Keys.ToArray<PlantProperty>())
        {
            currentResource[key] = baseResource[key];
        }
    }

    public void AddResource(Dictionary<PlantProperty, int> resource)
    {
        foreach (var pair in resource)
            currentResource[pair.Key] += pair.Value;
        
    }
    public void UnlockPlant(HelperPlantType type)
    {
        isPlantUnlocked[type] = true;
        HUD.Instance.UpdatePlantButtons();
    }
    public void UnlockResource(PlantProperty type)
    {
        isResourceUnlocked[type] = true;
    }
    


    public bool isIncreasingResource(PlantProperty p)
    {
        return !(p == PlantProperty.bee || p == PlantProperty.pest || p == PlantProperty.frog);
    }
}
