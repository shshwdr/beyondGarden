using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SerializedLevel : SerializedObject
{
    public int water;
}

public class LevelManager : SerializableObject
{
    string levelName = "apple";
    // Start is called before the first frame update
    public override SerializedObject Save()
    {
        var res = new SerializedLevel();
        res.water = PlantsManager.Instance.currentResource[PlantProperty.water];
        return res;
    }

    public override void Load(SerializedObject obj)
    {
        if (obj is SerializedLevel)
        {
            SerializedLevel sObj = (SerializedLevel)obj;
            PlantsManager.Instance.currentResource[PlantProperty.water] = sObj.water;
        }
    }
}
