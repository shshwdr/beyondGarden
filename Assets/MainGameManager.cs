using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager>
{
    [HideInInspector]
    public Transform allInTreeGame;
    public List<HelperPlant> plantedPlant = new List<HelperPlant>();
    // put trees to the correct position
    void Start()
    {
        allInTreeGame = GameObject.Find("allInTreeGame").transform;
        if (PlantsManager.Instance. serializedPlantedPlant.Count>0)
        {
            //move tree to correct position
            foreach(var sPlant in PlantsManager.Instance.serializedPlantedPlant)
            {
                var prefab = PlantsManager.Instance.helperPlantDict[sPlant.type];
                var go = Instantiate(prefab, sPlant.pos.GetPos(), Quaternion.identity, allInTreeGame);
                HelperPlant hp = go.GetComponent<HelperPlant>();
                hp.loadFromData(sPlant);
                plantedPlant.Add(go.GetComponent<HelperPlant>());
            }
        }


        PlantsManager.Instance.groundCollider1 = GameObject.Find("groundCollider1").GetComponent<Collider2D>();
        PlantsManager.Instance.groundCollider2 = GameObject.Find("groundCollider2").GetComponent<Collider2D>();
    }

    public List<Transform> plantsList()
    {
        List<Transform> res = new List<Transform>();
        foreach (var plantValue in plantedPlant)
        {
            if (plantValue && plantValue.isAlive && !plantValue.ignorePest)
            {
                res.Add(plantValue.transform);

            }
        }
        if (PlantsManager.Instance. maintree && PlantsManager.Instance.maintree.isAlive)
        {
            res.Add(PlantsManager.Instance.maintree.transform);
        }
        return res;
    }

    public void serializeData()
    {
        PlantsManager.Instance.serializedPlantedPlant.Clear();
        foreach (var plant in plantedPlant)
        {
            var sPlant = plant.Save() as SerializedHelperPlant;
            PlantsManager.Instance.serializedPlantedPlant.Add(sPlant);

        }
        PlantsManager.Instance.serializedMainTree = PlantsManager.Instance.maintree.serialize();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
