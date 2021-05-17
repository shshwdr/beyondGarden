using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashCollider : MonoBehaviour
{
    BossController bossController;
    // Start is called before the first frame update
    void Start()
    {
        bossController = GetComponentInParent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collider = collision.collider;
        if (bossController.isDashing && collider.GetComponent<FakeObstacles>())
        {
            collider.GetComponent<FakeObstacles>().getCollide(Vector3.zero);
        }
        if (bossController.isDashing && collider.GetComponent<PlayerController>())
        {
            collider.GetComponent<PlayerController>().getDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bossController.isDashing && collision.GetComponent<FakeObstacles>())
        {
            collision.GetComponent<FakeObstacles>().getCollide(Vector3.zero);
        }
        if (bossController.isDashing && collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().getDamage();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (bossController.isDashing && collision.GetComponent<FakeObstacles>())
        {
            collision.GetComponent<FakeObstacles>().getCollide(Vector3.zero);
        }
        if (bossController.isDashing && collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().getDamage();
        }
    }
}
