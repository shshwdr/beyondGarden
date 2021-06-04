using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        int randomValue = UnityEngine.Random.Range(0, transforms.Count);
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
        var randValue = UnityEngine.Random.Range(0, total);
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
        var randValue = UnityEngine.Random.Range(0, total);
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

        /// <summary>
        /// Gets the inner type of the given type, if it is an Array or has generic arguments. 
        /// Otherwise NULL.
        /// </summary>
        public static Type GetInnerListType(Type source)
        {
            Type innerType = null;

            if (source.IsArray)
            {
                innerType = source.GetElementType();
            }
            else if (source.GetGenericArguments().Any())
            {
                innerType = source.GetGenericArguments()[0];
            }

            return innerType;
        }
    static public List<Transform> reservoirSamplingTransformChild(Transform parent, int k)
    {
        List<Transform> transforms = new List<Transform>();
        foreach(Transform child in parent)
        {
            transforms.Add(child);
        }
        return reservoirSampling<Transform>(transforms, k);
    }
    static public List<T> reservoirSampling<T>(List<T> input, int k)
    {
        List<T> res = new List<T>();
        if (k >= input.Count)
        {
            return input;
        }
        for(int i = 0; i < k; i++)
        {
            res.Add(input[i]);
        }
        for(int i = k;i< input.Count; i++)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            if (r < k)
            {
                res[r] = input[i];
            }
        }
        return res;
    }
}
