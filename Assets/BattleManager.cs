using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    List<string> selectWeapons = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setSelectedWeapons(List<string> s)
    {
        selectWeapons = s;
    }
    public List<string> getSelectedWeapons()
    {
        return selectWeapons;
    }

    public FlowerInfo getCurrentWeapon()
    {
        return JsonManager.Instance.getFlower( selectWeapons[0]);
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
