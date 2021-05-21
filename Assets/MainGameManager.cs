using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager>
{
    [HideInInspector]
    public Transform allInTreeGame;
    // put trees to the correct position
    void Start()
    {
        allInTreeGame = GameObject.Find("allInTreeGame").transform;
        if (PlantsManager.Instance.serializedPlantedPlant.Count>0)
        {
            //move tree to correct position
            foreach(var sPlant in PlantsManager.Instance.serializedPlantedPlant)
            {
                var prefab = PlantsManager.Instance.helperPlantDict[sPlant.type];
                var go = Instantiate(prefab, sPlant.pos.GetPos(), Quaternion.identity, allInTreeGame);
                HelperPlant hp = go.GetComponent<HelperPlant>();
                hp.loadFromData(sPlant);
            }
        }

        PlantsManager.Instance.groundCollider1 = GameObject.Find("groundCollider1").GetComponent<Collider2D>();
        PlantsManager.Instance.groundCollider2 = GameObject.Find("groundCollider2").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
