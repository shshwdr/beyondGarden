using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCast : MonoBehaviour
{
    int nextSpell = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i <=2; i++)
        {

            if (Input.GetKeyDown(KeyCode.Alpha0+i)){
                nextSpell = i;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject spellPrefab = null;
            switch (nextSpell)
            {
                case 1:
                    spellPrefab = Resources.Load<GameObject>("spells/fireball");
                    break;
                case 2:


                    spellPrefab = Resources.Load<GameObject>("spells/virus");
                    break;
            }
            if (spellPrefab!=null)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                Instantiate(spellPrefab, mousePosition, spellPrefab.transform.rotation);

            }
        nextSpell = -1;
        }
    }
}
