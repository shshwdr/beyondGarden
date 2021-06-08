using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Transform weaponList;
    // Start is called before the first frame update
    void Start()
    {
        var selectWeapons = BattleManager.Instance.getSelectedWeapons();
        if (selectWeapons.Count > 0)
        {
            int i = 0;
            for (;i< selectWeapons.Count; i++)
            {
                var currentWeaponType = selectWeapons[i];
                var child = weaponList.GetChild(i);
                child.GetComponent<BattleWeaponCell>().init(currentWeaponType,i+1);
                child.gameObject.SetActive(true);
                //var currentWeapon = JsonManager.Instance.getPlant(currentWeaponType);
                //currentImage.sprite = currentWeapon.sprite;
            }
            for(;i< weaponList.childCount; i++)
            {
                var child = weaponList.GetChild(i);
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
