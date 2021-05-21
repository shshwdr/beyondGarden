using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    public Transform flowersContent;
    public Transform selectedContent;
    public Button goButton;
    public Button returnButton;
    // Start is called before the first frame update
    void Start()
    {
        goButton.onClick.AddListener(delegate {
            PlantsManager.Instance.gotoDungeon();
            SceneManager.LoadScene(1);
        });
        returnButton.onClick.AddListener(delegate {
            gameObject.SetActive(false);
        });

    }

    public void showMenu()
    {
        gameObject.SetActive(true);
        int i = 0;
        foreach(var k in PlantsManager.Instance.plantUpgradeStatusDict.Keys)
        {

            flowersContent.GetChild(i).gameObject.SetActive(true);
            var script = flowersContent.GetChild(i).gameObject.GetComponent<SelectFlowerWeaponCell>();
            script.UpdateInfo(k);
            i++;
        }
        for(;i< flowersContent.childCount; i++)
        {
            flowersContent.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
