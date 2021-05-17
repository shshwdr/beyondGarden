using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<EnemyController>())
        {
            collision.collider.GetComponent<EnemyController>().getDamage();
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>())
        {
            collision.GetComponent<EnemyController>().getDamage();
        }
    }
}
