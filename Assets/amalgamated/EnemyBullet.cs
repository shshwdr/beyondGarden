using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public bool ignoreOtherCollider = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            collision.collider.GetComponent<PlayerController>().getDamage();
            Destroy(gameObject);
        }
        else if (ignoreOtherCollider)
        {
            return;
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().getDamage();
            Destroy(gameObject);
        }
        else if (collision. GetComponentInParent<PlayerController>())
        {
            collision.GetComponentInParent<PlayerController>().getDamage();
            Destroy(gameObject);
        }
    }
}
