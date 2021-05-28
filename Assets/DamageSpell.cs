using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpell : SpellBase
{
    public float baseDamage = 1;
    public string element = "Fire";
    public void activate(HPCharacterController attackee)
    {
        attackee.getDamage((int)baseDamage, element);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
