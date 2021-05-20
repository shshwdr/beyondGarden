using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class PairInfo
{
    public string Key;
    public int Value;
}
[Serializable]
public class InfoObject { }
[Serializable]


public class ItemInfo
{
    public string id;
    public string name;
    public string desc;
    public int initialValue;
}
[Serializable]
public class CurrencyInfo : ItemInfo
{
    public Sprite sprite
    {
        get
        {
            var str = "art/currency/Icon " + id;
            Sprite t = Resources.Load<Sprite>(str);
            return t;// Resources.Load("art/currency/Icon " + id) as Sprite;
        }
    }
}
[Serializable]
public class SeedInfo:ItemInfo
{
}
[Serializable]
public class PlantInfo
{
    public string id;
    public string name;
    public string latinName;
    public string desc;
    public bool locked;
    public float harvestTime;
    public List<PairInfo> produces;
    public List<PairInfo> plantCost;

    public Sprite sprite { get { return Resources.Load("art/flowers/" + id) as Sprite; } }
}

[Serializable]
public class FlowerInfo : PlantInfo
{
}
[Serializable]
public class TreeInfo : PlantInfo
{
}
public class AllFlowersInfo
{
    public List<FlowerInfo> flowers;
    public List<TreeInfo> trees;
}
public class AllResourcesInfo
{
    public List<CurrencyInfo> currency;
    public List<SeedInfo> seed;
}
public class JsonManager : Singleton<JsonManager>
{
    public Dictionary<string, FlowerInfo> flowerDict;
    public Dictionary<string, TreeInfo> treeDict;
    public Dictionary<string, CurrencyInfo> currencyDict;
    public Dictionary<string, SeedInfo> seedDict;
    private void Awake()
    {
        //flowers

        string text = Resources.Load<TextAsset>("json/plants").text;
        AllFlowersInfo allFlowersInfoList = JsonUtility.FromJson<AllFlowersInfo>(text);
        flowerDict = allFlowersInfoList.flowers.ToDictionary(x => x.id, x => x);
        treeDict = allFlowersInfoList.trees.ToDictionary(x => x.id, x => x);
        //resource

        text = Resources.Load<TextAsset>("json/resources").text;
        AllResourcesInfo allResourcesInfoList = JsonUtility.FromJson<AllResourcesInfo>(text);
        currencyDict = allResourcesInfoList.currency.ToDictionary(x => x.id, x => x);
        seedDict = allResourcesInfoList.seed.ToDictionary(x => x.id, x => x);
    }

    public PlantInfo getPlant(string type)
    {
        if (flowerDict.ContainsKey(type))
        {
            return flowerDict[type];
        }
        if (treeDict.ContainsKey(type))
        {
            return treeDict[type];
        }
        Debug.LogError(type + " does not exist");
        return null;
    }

    public CurrencyInfo getCurrency(string type)
    {
        if (currencyDict.ContainsKey(type))
        {
            return currencyDict[type];
        }
        Debug.LogError(type + " does not exist");
        return null;
    }
}
