using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Inventory : Singleton<Inventory>
{
    Dictionary<string, int> seed = new Dictionary<string, int>();
    Dictionary<string, int> resources = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        //if no saved data, read origin data
        foreach(var pair in JsonManager.Instance.seedDict)
        {
            seed[pair.Key] = pair.Value.initialValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(List<PairInfo> resource)
    {
        foreach(var pair in resource)
        {
            var type = pair.Key;
            var value = pair.Value;
            if (JsonManager.Instance.seedDict.ContainsKey(type))
            {
                addSeed(type, value);
            }
            else
            {
                Debug.LogError("type does not support");
            }
        }
    }
    public void addSeed(string type, int amount = 1)
    {
        Assert.IsTrue(seed.ContainsKey(type));
        seed[type] += amount;
    }

    public void useSeed(string type, int amount = 1)
    {
        Assert.IsTrue(seed.ContainsKey(type));
        seed[type] -= amount;
    }

    public bool hasEnoughSeed(string type, int amount = 1)
    {
        return seed.ContainsKey(type) && seed[type] >= amount;
    }

    public int getSeedAmount(string type)
    {
        return seed.ContainsKey(type)?seed[type]:0;
    }
}
