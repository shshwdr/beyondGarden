using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFlower : MonoBehaviour
{
    public GameObject flowerScentPrefab;
    GameObject flowerScent;
    public GameObject fruitPrefab;
    [HideInInspector]
    public MainTree tree;
    bool isPollinated;
    bool isDragging;
    int indexOnTree;
    string type;

    public void init(MainTree t,int i)
    {
        tree = t;
        type = t.treeInfo.flowerId;
        indexOnTree = i;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        flowerScent = Instantiate(flowerScentPrefab, mousePosition, Quaternion.identity);
        flowerScent.GetComponent<FlowerScent>().treeFlower = this;
    }


    public void GetPollinate()
    {
        if (isPollinated)
        {
            return;
        }
        isPollinated = true;
        Instantiate(fruitPrefab, transform.position, Quaternion.identity,transform.parent);
        tree.createFruit(indexOnTree);
        PlantsManager.Instance.addExpForFlowerWeapon(type, transform);
        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        
        flowerScent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging && flowerScent && !isPollinated)
        {

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            flowerScent.transform.position = mousePosition;

        }
    }
}
