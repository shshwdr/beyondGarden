using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class PairInfo<T>
{
    public string Key;
    public T Value;
    public PairInfo(string k, T v)
    {
        Key = k;
        Value = v;
    }
}
[Serializable]
public class InfoObject
{
    public string id;
}
[Serializable]
public class ItemInfo : InfoObject
{
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
public class PlantInfo : ItemInfo
{
    public string latinName;
    public bool locked;
    public List<PairInfo<int>> plantCost;

    public override Sprite sprite { get { return Resources.Load<Sprite>("art/flowers/" + id) as Sprite; } }
}

[Serializable]
public class EnemyInfo : InfoObject
{
    public string name;
    public string type;
    public float attack;
    public float attackIncrease;
    public float hp;
    public float hpIncrease;
    public List<PairInfo<float>> drop;
}
[Serializable]
public class DungeonInfo : InfoObject
{

    public string name;
    public string keyElement;
}

[Serializable]
public class DungeonLevelInfo : InfoObject
{
    public string dungeonId;
    public List<string> enemies;
    public List<int> levels;
    //public int enemyTypeCount;
    public List<int> enemyCount;
    public string keyElement;
}

[Serializable]
public class FlowerInfo : PlantInfo
{
    public float harvestTime;
    public List<PairInfo<int>> produces;
    public float attack;
    public float attackIncrease;
    public int upgradeExpNeeded;
    public string spell;
    public float spellCoolDown;
    public float spellAttack;
    public float spellAttackIncrease;
    public float seedDropRate;
    public bool isTreeFlower;


    public Sprite spellSprite { get { return Resources.Load<Sprite>("art/spells/" + spell) as Sprite; } }
    public int expForLevel(int level)
    {
        return upgradeExpNeeded * level * 2;
    }
    public string weaponType { get { return JsonManager.productToWeaponType[produces[0].Key]; } }
    public int getAttack
    {
        get { return (int)Math.Floor(attack); }
    }
}

[Serializable]
public class TreeInfo : PlantInfo
{
    public string nextLevel;
    public string flowerId;
    public int fruitCountToFinish;
    public string treeType;
    public string effect;
    public int levelOfTree;
    public bool isMaxLevel { get { return nextLevel == ""; } }

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

    static public Dictionary<string, string> productToWeaponType = new Dictionary<string, string>()
    {
        {"frog","Water" },
        {"bee","Dark" },
        {"tree","Light" },
        {"n","Fire" },
        {"mulch","Earth" },

    };
    public Dictionary<string, FlowerInfo> flowerDict;
    public Dictionary<string, EnemyInfo> enemyDict;
    public Dictionary<string, TreeInfo> treeDict;
    public Dictionary<string, CurrencyInfo> currencyDict;
    public Dictionary<string, DungeonInfo> dungeonDict;
    public Dictionary<string, List<DungeonLevelInfo>> dungeonLevelDict;
    private void Awake()
    {

        string text = Resources.Load<TextAsset>("json/resources").text;
        AllResourcesInfo allResourcesInfoList = JsonUtility.FromJson<AllResourcesInfo>(text);
        currencyDict = allResourcesInfoList.currency.ToDictionary(x => x.id, x => x);

        //flower
        text = Resources.Load<TextAsset>("json/flowers").text;
        var flowers = Sinbad.CsvUtil.LoadObjects<FlowerInfo>(text);
        flowerDict = flowers.ToDictionary(x => x.id, x => x);

        //tree

        text = Resources.Load<TextAsset>("json/trees").text;
        var trees = Sinbad.CsvUtil.LoadObjects<TreeInfo>(text);
        treeDict = trees.ToDictionary(x => x.id, x => x);

        //enemy

        text = Resources.Load<TextAsset>("json/enemies").text;
        var enemies = Sinbad.CsvUtil.LoadObjects<EnemyInfo>(text);
        enemyDict = enemies.ToDictionary(x => x.id, x => x);

        //dungeon
        text = Resources.Load<TextAsset>("json/dungeons").text;
        var dungeons = Sinbad.CsvUtil.LoadObjects<DungeonInfo>(text);
        dungeonDict = dungeons.ToDictionary(x => x.id, x => x);

        //dungeon levels
        text = Resources.Load<TextAsset>("json/dungeonLevels").text;
        var dungeonLevels = Sinbad.CsvUtil.LoadObjects<DungeonLevelInfo>(text);
        dungeonLevelDict = new Dictionary<string, List<DungeonLevelInfo>>();
        foreach (var level in dungeonLevels)
        {
            var dungeonId = level.dungeonId;
            if (!dungeonLevelDict.ContainsKey(dungeonId))
            {
                dungeonLevelDict[dungeonId] = new List<DungeonLevelInfo>();
            }

            dungeonLevelDict[dungeonId].Add(level);
        }
    }

    //public DungeonLevelInfo

    public EnemyInfo getEnemy(string id)
    {
        if (enemyDict.ContainsKey(id))
        {
            return enemyDict[id];
        }

        Debug.LogError(id + " enemy does not exist");
        return null;
    }

    public bool isFlower(string type)
    {
        return (flowerDict.ContainsKey(type));
    }

    public FlowerInfo getFlower(string type)
    {
        if (flowerDict.ContainsKey(type))
        {
            return flowerDict[type];
        }
        Debug.LogError(type + " flower does not exist");
        return null;
    }

    public TreeInfo getTree(string type)
    {
        if (treeDict.ContainsKey(type))
        {
            return treeDict[type];
        }
        Debug.LogError(type + " tree does not exist");
        return null;
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
        Debug.LogError(type + " plant does not exist");
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
        Debug.LogError(type + " item does not exist");
        return null;
    }
    public List<DungeonLevelInfo> getDungeonLevels(string dungeonId)
    {
        if (dungeonLevelDict.ContainsKey(dungeonId))
        {
            var dungeonLevelList = dungeonLevelDict[dungeonId];
            return dungeonLevelList;

        }
        else
        {
            Debug.LogError(dungeonId + " dungeon id does not exist");
            return null;
        }
    }
    public DungeonLevelInfo getDungeonLevel(string dungeonId, int level)
    {
        var dungeonLevelList = getDungeonLevels(dungeonId);
        if (dungeonLevelList != null && level < dungeonLevelList.Count)
        {
            return dungeonLevelList[level];
        }

        Debug.LogError(level + " does not exist in dungeon " + dungeonId);
        return null;
    }
    public CurrencyInfo getCurrency(string type)
    {
        if (currencyDict.ContainsKey(type))
        {
            return currencyDict[type];
        }
        Debug.LogError(type + " currency does not exist");
        return null;
    }
}
