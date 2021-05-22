using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    public Transform flowersContent;
    public Transform selectedContent;
    public Button goButton;
    public Button returnButton;

    public TMP_Text detailTitle;
    public Image detailImage;
    public InfoPair detailType;
    public InfoPair detailLevel;
    public InfoPair detailAttack;
    public InfoPair detailSpell;


    List<string> selectedWeapon = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        goButton.onClick.AddListener(delegate {
            if(selectedWeapon.Count > 0)
            {
                BattleManager.Instance.setSelectedWeapons(selectedWeapon);
                PlantsManager.Instance.gotoDungeon();
                SceneManager.LoadScene(1);
            }
        });
        returnButton.onClick.AddListener(delegate {
            gameObject.SetActive(false);
        });

    }

    public void selectWeapon(SelectFlowerWeaponCell cell)
    {
        if (cell.isSelected)
        {
            cell.transform.SetParent(flowersContent);
            selectedWeapon.Remove(cell.flowerType);
        }
        else
        {
            //todo support multiple weapons
            Utils.ClearChildren(selectedContent);
            cell.transform.SetParent(selectedContent);
            selectedWeapon.Add(cell.flowerType);

        }
        cell.isSelected = !cell.isSelected;
    }

    public void updateInfo()
    {

    }

    public void showMenu()
    {
        gameObject.SetActive(true);
        int i = 0;
        foreach(var k in PlantsManager.Instance.plantUpgradeStatusDict.Keys)
        {

            flowersContent.GetChild(i).gameObject.SetActive(true);
            var script = flowersContent.GetChild(i).gameObject.GetComponent<SelectFlowerWeaponCell>();
            script.UpdateInfo(k,this);
            i++;
        }
        for(;i< flowersContent.childCount; i++)
        {
            flowersContent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void updateDetailInfo(string type)
    {
        var weaponInfo = JsonManager.Instance.getPlant(type) as FlowerInfo;
        if (weaponInfo!=null)
        {
            detailTitle.text = weaponInfo.name;
            detailImage.sprite = weaponInfo.sprite;
            detailLevel.updateValue("1");
            detailType.updateValue(weaponInfo.weaponType);
            detailAttack.updateValue(weaponInfo.getAttack.ToString());
            detailSpell.updateValue(weaponInfo.spell);
        }
        else
        {
            Debug.LogError("weapon does not exist for " + type);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
