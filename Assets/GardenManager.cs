using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : Singleton<GardenManager>
{
    public Transform gardenSlots;
    public bool alwaysUpdateTree;
   


    public void CopyTreeToGarden(string treeType)
    {
        foreach (Transform slot in gardenSlots)
        {
            var slotTreeId = slot.GetComponent<GardenSlot>().tree.GetComponent<MainTree>().type;
            var slotTreeType = JsonManager.Instance.getTree(slotTreeId).treeType;
            if (slotTreeType == treeType)
            {
                //1. clear original garden info
                foreach (Transform child in slot.GetComponent<GardenSlot>().allInTreeNode)
                {
                    Destroy(child.gameObject);
                }
                //List<Transform> allPlants = new List<Transform>();
                foreach (Transform plant in MainGameManager.Instance.allInTreeGame)
                {
                    var newPlant = Instantiate(plant.gameObject);
                    var t = newPlant.transform;
                    var localP = t.localPosition;
                    t.SetParent(slot.GetComponent<GardenSlot>().allInTreeNode);
                    t.localPosition = localP;
                    foreach(Transform c in t)
                    {
                        foreach (Transform c2 in c)
                        {
                            foreach (Transform c3 in c2)
                            {
                                //this is to remove.. resource?
                                //foreach (Transform c4 in c)
                                {
                                    if (c3.GetComponent<HelperInsect>())
                                    {
                                        Destroy(c3.gameObject);
                                    }
                                }
                            }
                        }
                    }
                    t.GetComponent<HelperPlant>().MoveToGarden();
                }
            }
        }
    }

    public void StartTree(GardenSlot slot)
    {
        //move all tree from garden to tree mode
        foreach (Transform child in MainGameManager.Instance.allInTreeGame)
        {
            Destroy(child.gameObject);
        }
        PlantsManager.Instance.mainTreePrefab = slot.tree;
        //Instantiate(slot.tree, PlantsManager.Instance.allInTreeGame);
        HUD.Instance.MoveToTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
