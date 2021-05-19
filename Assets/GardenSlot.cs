using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenSlot : MonoBehaviour
{
    [HideInInspector]
    public string finishedTreeType;
    public GameObject tree;
    public Transform allInTreeNode;

    private void OnMouseEnter()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        HUD.Instance.showLevelInfo(tree.GetComponent<MainTree>().type);
    }

    private void OnMouseExit()
    {
        HUD.Instance.hideLevelInfo();
    }

    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Popup.Instance.Init(Dialogues.StartTreeConfirm, () =>
        {
            Debug.Log(tree);
            GardenManager.Instance.StartTree(this);

            PlantsManager.Instance.background.sprite = tree.GetComponent<MainTree>().background;
            HUD.Instance.clearLevel();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        finishedTreeType = tree.GetComponent<MainTree>().finishType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
