using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleWeaponCell : MonoBehaviour
{
    string flowerId;
    int index;
    string spellId;
    public TMP_Text indexText;
    public Image spellSprite;
    public Image flowerSprite;
    public GameObject selectedFrame;
    private Action<EventParam> weaponChangeListener;
    // Start is called before the first frame update
    void Start()
    {
        selectedFrame.SetActive(false);
        weaponChangeListener = new Action<EventParam>(selectedWeapon);
    }

    public void selectedWeapon(EventParam param)
    {
        print("select " + param.param2);
        if (BattleManager.Instance.currentWeaponId == index)
        {

            selectedFrame.SetActive(true);
        }
    }

    public void init(string id,int i)
    {
        flowerId = id;
        index = i;
        var currentWeapon = JsonManager.Instance.getFlower(flowerId);
        spellId = currentWeapon.spell;

        flowerSprite.sprite = currentWeapon.sprite;
        spellSprite.sprite = currentWeapon.spellSprite;
        indexText.text = i.ToString();


        if (BattleManager.Instance.currentWeaponId == index)
        {

            selectedFrame.SetActive(true);
        }

        EventManager.StartListening("selectWeapon", weaponChangeListener);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0 + index))
        {
            BattleManager.Instance.selectWeapon(index);
            //PlayerSpellCast.Instance.nextSpell = spellId;

        }
    }
}
