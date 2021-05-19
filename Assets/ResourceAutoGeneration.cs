using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAutoGeneration : Singleton<ResourceAutoGeneration>
{
    //public float harvestTime = 10;
    //float currentHarvestTimer;
    //public Transform spawnPositionParent;
    //public Transform pestParent;
    //List<Transform> transforms;
    //public bool autoResrouceStart = false;
    //public void Clear()
    //{
    //    foreach (Transform t in pestParent)
    //    {
    //        Destroy(t.gameObject);
    //    }
    //    currentHarvestTimer = 0;
    //}

    //Dictionary<PlantProperty, int> autoResrouce = new Dictionary<PlantProperty, int>
    //{
    //    {PlantProperty.water,20 },

    //};
    //void Harvest()
    //{

    //    var t = Utils.RandomTransform(spawnPositionParent);
    //    var go = Instantiate(PlantsManager.Instance.ClickToCollect, t.position, Quaternion.identity, pestParent);
    //    var box = go.GetComponent<ClickToCollect>();
    //    PlantProperty[] dropProperties = new PlantProperty[]{
    //        PlantProperty.n, PlantProperty.s, PlantProperty.p, PlantProperty.water
    //    };
    //    var typeRandom = Random.Range(0, 4);

    //    box.resource = new Dictionary<PlantProperty, int>() {
    //        {dropProperties[typeRandom], Random.Range(5, 10)  },
    //    };
    //    box.dropboxType = DropboxType.resource;


    //    box.UpdateImage();
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!autoResrouceStart)
    //    {
    //        return;
    //    }
    //    if (currentHarvestTimer > harvestTime)
    //    {
    //        currentHarvestTimer = 0;
    //        Harvest();
    //    }
    //    currentHarvestTimer += Time.deltaTime;
    //}
}
