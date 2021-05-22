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
    // Update is called once per frame
    void Update()
    {
        
    }
}
