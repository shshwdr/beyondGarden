﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : Singleton<GardenManager>
{
    public Transform gardenSlots;
    public bool alwaysUpdateTree;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    [ContextMenu("finishTree")]
    public void testFT()
    {
        finishTree(HelperPlantType.appleTree3);
    }
    public void finishTree(HelperPlantType treeType)
    {
        foreach (Transform slot in gardenSlots)
        {
            if (slot.GetComponent<GardenSlot>().finishedTreeType == treeType)
            {
                foreach (Transform child in slot.GetComponent<GardenSlot>().allInTreeNode)
                {
                    Destroy(child.gameObject);
                }
                //List<Transform> allPlants = new List<Transform>();
                foreach (Transform tt in PlantsManager.Instance.allInTreeGame)
                {
                    var go = Instantiate(tt.gameObject);
                    var t = go.transform;
                    var localP = t.localPosition;
                    t.SetParent(slot.GetComponent<GardenSlot>().allInTreeNode);
                    t.localPosition = localP;
                    t.GetComponent<HelperPlant>().MoveToGarden();
                }
            }
        }
    }

    public void StartTree(GardenSlot slot)
    {
        //move all tree from garden to tree mode
        foreach (Transform child in PlantsManager.Instance.allInTreeGame)
        {
            GameObject.Destroy(child.gameObject);
        }
        Instantiate(slot.tree, PlantsManager.Instance.allInTreeGame);
        HUD.Instance.MoveToTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
