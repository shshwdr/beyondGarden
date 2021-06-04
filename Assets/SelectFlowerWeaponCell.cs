using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFlowerWeaponCell : MonoBehaviour
{
    Image image;
    Button selectButton;
    WeaponSelection weaponSelection;
    public string flowerType;
    public bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateInfo(string key, WeaponSelection ws)
    {
        image = GetComponent<Image>();
        selectButton = GetComponent<Button>();
        flowerType = key;
        if (!weaponSelection)
        {

            weaponSelection = ws;

            selectButton.onClick.AddListener(delegate {
                weaponSelection.selectWeapon(this);
            });
        }
        image.sprite = JsonManager.Instance.getPlant(flowerType).sprite;
    }

    public void hover()
    {
        weaponSelection.updateDetailInfo(flowerType);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
