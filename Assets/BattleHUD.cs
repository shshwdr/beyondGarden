using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Image currentImage;
    // Start is called before the first frame update
    void Start()
    {
        if (BattleManager.Instance.getSelectedWeapons().Count > 0)
        {
            var currentWeaponType = BattleManager.Instance.getSelectedWeapons()[0];
            var currentWeapon = JsonManager.Instance.getPlant(currentWeaponType);
            currentImage.sprite = currentWeapon.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
