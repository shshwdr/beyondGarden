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

    //public static string randomList<T>(List<PairInfo<T>> infoList,T t)
    //{

    //    List<T> incrementRateList = new List<T>();
    //    dynamic incrementRate = 0;
    //    foreach (var info in infoList)
    //    {
    //        dynamic value = info.Value;
    //        incrementRate += value;
    //        incrementRateList.Add(incrementRate);
    //    }
    //    dynamic total = t;
    //    if(total.Equals(0))
    //    {
    //        total = incrementRate;
    //    }
    //    var randValue = Random.Range(0, total);
    //    for(int i = 0;i< incrementRateList.Count; i++)
    //    {
    //        if (randValue < incrementRateList[i])
    //        {

    //            return infoList[i].Key;
    //        }
    //    }
    //    Debug.Log("should not reach here");
    //    return infoList[0].Key;
    //}

    public static string randomList(List<PairInfo<int>> infoList, int t)
    {

        List <int > incrementRateList = new List<int>();
        int incrementRate = 0;
        foreach (var info in infoList)
        {
            incrementRate += info.Value;
            incrementRateList.Add(incrementRate);
        }
        int total = t;
        if (total.Equals(0))
        {
            total = incrementRate;
        }
        var randValue = Random.Range(0, total);
        for (int i = 0; i < incrementRateList.Count; i++)
        {
            if (randValue < incrementRateList[i])
            {

                return infoList[i].Key;
            }
        }
        Debug.Log("should not reach here");
        return infoList[0].Key;
    }

    public static string randomList(List<PairInfo<float>> infoList)
    {

        List<float> incrementRateList = new List<float>();
        float incrementRate = 0;
        foreach (var info in infoList)
        {
            incrementRate += info.Value;
            incrementRateList.Add(incrementRate);
        }
        float total = 1;
        var randValue = Random.Range(0, total);
        for (int i = 0; i < incrementRateList.Count; i++)
        {
            if (randValue < incrementRateList[i])
            {

                return infoList[i].Key;
            }
        }
        Debug.Log("should not reach here");
        return infoList[0].Key;
    }

}
