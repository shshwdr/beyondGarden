using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    List<string> selectWeapons = new List<string>();
    int currentWeaponId = 1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void initBattleManager()
    {
        currentWeaponId = 1;
    }
    public void setSelectedWeapons(List<string> s)
    {
        selectWeapons = s;
    }
    public List<string> getSelectedWeapons()
    {
        if (selectWeapons == null || selectWeapons.Count == 0)
        {
            //test
            selectWeapons = new List<string>() { "waterlily", "lupin", "lavender", "marigold", "AppleTreeFlower" };
        }
        return selectWeapons;
    }

    public FlowerInfo getCurrentWeapon()
    {
        if(selectWeapons == null || selectWeapons.Count == 0)
        {
            return null;
        }
        return JsonManager.Instance.getFlower( selectWeapons[currentWeaponId-1]);
    }

    public void selectWeapon(int index)
    {
        currentWeaponId = index;
    }

    public void selectNextWeapon()
    {

        if (selectWeapons == null || selectWeapons.Count == 0)
        {
            return;
        }
        currentWeaponId = (currentWeaponId + 1) % selectWeapons.Count;
    }
    public void selectPreviousWeapon()
    {

        if (selectWeapons == null || selectWeapons.Count == 0)
        {
            return;
        }
        currentWeaponId = (currentWeaponId - 1) % selectWeapons.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int finalDamage(string attackerElement, string attackeeElement, float damage)
    {
        //var attackee = JsonManager.Instance.getEnemy(attackeeId);
        var scale = elementAdvantageScale(attackerElement, attackeeElement);
        return Mathf.Max(1, Mathf.FloorToInt(damage * scale));

    }
    

    public static Dictionary<string, string> elementAdvantage = new Dictionary<string, string>()
    {
        {"Fire","Earth" },
        {"Earth","Water" },
        {"Water","Fire" },
        {"Light","Dark" },
        {"Dark","Light" },
    };

    public static float elementAdvantageScale(string attackerElement, string attackeeElement)
    {
        if (elementAdvantage.ContainsKey(attackerElement) && elementAdvantage[attackerElement] == attackeeElement)
        {
            return 2;
        }else if(elementAdvantage.ContainsKey(attackeeElement) && elementAdvantage[attackeeElement] == attackerElement)
        {
            return 0.5f;
        }
        return 1;
    }
}
