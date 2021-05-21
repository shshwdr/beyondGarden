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
    public PairInfo(string k, int v)
    {
        Key = k;
        Value = v;
    }
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
    public virtual Sprite sprite
    {
        get
        {
            return null;
        }
    }
}
[Serializable]
public class CurrencyInfo : ItemInfo
{
    public override Sprite sprite
    {
        get
        {
            var str = "art/currency/Icon " + id;
            Sprite t = Resources.Load<Sprite>(str);
            return t;
        }
    }
}

[Serializable]
public class PlantInfo: ItemInfo
{
    public string latinName;
    public bool locked;
    public float harvestTime;
    public List<PairInfo> produces;
    public List<PairInfo> plantCost;

    public override Sprite sprite { get { return Resources.Load<Sprite>("art/flowers/" + id) as Sprite; } }
}

[Serializable]
public class FlowerInfo : PlantInfo
{
    public float attack;
    public float attackIncrease;
    public int upgradeExpNeeded;
    public string spell;
    public float spellCoolDown;
    public float spellAttack;
    public float spellAttackIncrease;
    public float seedDropRate;
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
}
public class JsonManager : Singleton<JsonManager>
{
    public Dictionary<string, FlowerInfo> flowerDict;
    public Dictionary<string, TreeInfo> treeDict;
    public Dictionary<string, CurrencyInfo> currencyDict;
    private void Awake()
    {
        //flowers

        string text = Resources.Load<TextAsset>("json/plants").text;
        AllFlowersInfo allFlowersInfoList = JsonUtility.FromJson<AllFlowersInfo>(text);
       // flowerDict = allFlowersInfoList.flowers.ToDictionary(x => x.id, x => x);
        treeDict = allFlowersInfoList.trees.ToDictionary(x => x.id, x => x);
        ////resource

        text = Resources.Load<TextAsset>("json/resources").text;
        AllResourcesInfo allResourcesInfoList = JsonUtility.FromJson<AllResourcesInfo>(text);
        currencyDict = allResourcesInfoList.currency.ToDictionary(x => x.id, x => x);

        string t = true.ToString();
        text = Resources.Load<TextAsset>("json/flowers").text;
        var flowers = Sinbad.CsvUtil.LoadObjects<FlowerInfo>(text);
        flowerDict = flowers.ToDictionary(x => x.id, x => x);
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

    public ItemInfo getItemInfo(string type)
    {
        if (currencyDict.ContainsKey(type))
        {
            return currencyDict[type];
        }
        if (flowerDict.ContainsKey(type))
        {
            return flowerDict[type];
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
