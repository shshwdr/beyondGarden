using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleWeaponCell : MonoBehaviour
{
    string flowerId;
    int index;
    string spellId;
    public TMP_Text indexText;
    public Image spellSprite;
    public Image flowerSprite;
    // Start is called before the first frame update
    void Start()
    {
        
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
