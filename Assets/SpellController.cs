using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyController>();
        if (enemy)
        {
            if (GetComponent<DamageSpell>())
            {

                GetComponent<DamageSpell>().activate(enemy);
            }
            if (GetComponent<ExplodeSpell>())
            {

                GetComponent<ExplodeSpell>().activate(enemy);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
