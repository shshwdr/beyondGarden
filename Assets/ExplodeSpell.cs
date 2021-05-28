using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSpell : MonoBehaviour
{
    public float pushForce = 100;
    public void activate(HPCharacterController attackee)
    {
        var rb = attackee.GetComponent<Rigidbody2D>();
        if (rb)
        {
            Vector2 vec = (Vector2)(attackee.transform.position - transform.position);
            var dir = vec.normalized;
            rb.AddForce(dir * pushForce);
        }

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
