using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    Dictionary<string, int> seed = new Dictionary<string, int>();
    Dictionary<string, int> resources = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        //if no saved data, read origin data
        foreach (var pair in JsonManager.Instance.flowerDict)
        {
            seed[pair.Key] = pair.Value.initialValue;
        }
    }

    public List<string> unlockedSeed()
    {
        var res = new List<string>();
        foreach(var key in seed.Keys)
        {
            var flowerInfo = JsonManager.Instance.getFlower(key);
            if (!flowerInfo.isTreeFlower)
            {
                res.Add(key);
            }
        }
        return res;
    }

    public List<string> dropableResource()
    {
        return new List<string>() { "p", "n", "mulch","water" };
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
