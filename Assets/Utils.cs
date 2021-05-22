using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public static float SuperLerp(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
            return from;
        else if (value >= to2)
            return to;
        return (to - from) * ((value - from2) / (to2 - from2)) + from;
    }
    public static Transform RandomTransform(Transform tranformParent)
    {
        List<Transform> trans = new List<Transform>();
        foreach(Transform c in tranformParent)
        {
            trans.Add(c);
        }
        return RandomTransform(trans);
    }

    public static void ClearChildren(Transform transformParent)
    {
        foreach(Transform c in transformParent)
        {
            Destroy(c.gameObject);
        }
    }

    public static void setChildrenToInactive(Transform transformParent)
    {
        foreach (Transform c in transformParent)
        {
            c.gameObject.SetActive(false);
        }
    }

    public static Transform RandomTransform(List<Transform> transforms)
    {
        int randomValue = Random.Range(0, transforms.Count);
        return transforms[randomValue];
    }


        // Update is called once per frame
        void Update()
    {
        
    }
}
