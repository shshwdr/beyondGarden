using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCast : Singleton<PlayerSpellCast>
{
    public string nextSpell = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject spellPrefab = null;
            if(nextSpell!=null && nextSpell.Length > 0)
            {

                spellPrefab = Resources.Load<GameObject>("spells/"+nextSpell);
            }
            if (spellPrefab!=null)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                Instantiate(spellPrefab, mousePosition, spellPrefab.transform.rotation);

            }
        nextSpell = "";
        }
    }
}
