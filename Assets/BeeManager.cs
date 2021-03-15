﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : Singleton<BeeManager>
{
    public GameObject bee;
    public Transform beeSpawnPositionParent;
    List<Transform> beeSpawnPositions = new List<Transform>();
     float beeGenerateTime = -5;
    float currentBeeGenerateTime = 0;
    [HideInInspector]
    public int currentBeeCount = 0;
    public int maxBeeCount = 5;

    public Transform getClosestSpawnTransform(Vector3 pos)
    {
        float dis = 1000;
        Transform res = null;
        foreach (Transform p in beeSpawnPositionParent)
        {
            if((pos - p.position).magnitude < dis)
            {
                dis = (pos - p.position).magnitude;
                res = p;
            }
        }
        return res;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform p in beeSpawnPositionParent)
        {
            beeSpawnPositions.Add(p);
        }
    }

    public void updateGenerateTime()
    {
        var beeValue = PlantsManager.Instance.currentResource[PlantProperty.bee];
        if(beeValue == 0)
        {
            beeGenerateTime = -1;
            return;
        }
        beeGenerateTime = Utils.SuperLerp(20, 3, 0, 10, beeValue);
    }

    // Update is called once per frame
    void Update()
    {
        if(beeGenerateTime > 0)
        {

            currentBeeGenerateTime += Time.deltaTime;
            if (currentBeeGenerateTime >= beeGenerateTime && currentBeeCount < maxBeeCount)
            {
                currentBeeGenerateTime = 0;
                currentBeeCount++;
                Instantiate(bee, Utils.RandomTransform(beeSpawnPositionParent).position, Quaternion.identity);
            }
        }
    }
}
