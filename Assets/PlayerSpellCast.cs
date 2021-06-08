using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCast : Singleton<PlayerSpellCast>
{
    float cooldownTime = 1f;
    float currentCooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {

            BattleManager.Instance.selectNextWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            BattleManager.Instance.selectPreviousWeapon();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (currentCooldownTimer < cooldownTime)
            {
                return;
            }
            currentCooldownTimer = 0;
            GameObject spellPrefab = null;
            string spellId = BattleManager.Instance.getCurrentWeapon().spell;
            spellPrefab = Resources.Load<GameObject>("spells/"+ spellId);
            if (spellPrefab!=null)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                Instantiate(spellPrefab, mousePosition, spellPrefab.transform.rotation);

            }
        }
    }
}
