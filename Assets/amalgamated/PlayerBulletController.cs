using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    float damage = 1;
    string element = "";

    public void init(float d,string e)
    {
        damage = d;
        element = e;
    }
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
        var enemyController = collision.collider.GetComponent<EnemyController>();
        if (enemyController)
        {
            var scale = BattleManager.elementAdvantageScale(element, enemyController.elementType);
            var finalDamage = BattleManager.finalDamage(element, enemyController.elementType, damage);
            enemyController.getDamage(finalDamage);

            PopupTextManager.Instance.ShowPopupNumber(transform.position, finalDamage, scale);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var enemyController = collision.GetComponent<EnemyController>();
        //if (collision.GetComponent<EnemyController>())
        //{
        //    var scale = BattleManager.elementAdvantageScale(element, enemyController.elementType);
        //    var finalDamage = BattleManager.finalDamage(element, enemyController.elementType, damage);
        //    enemyController.getDamage(finalDamage);

        //    PopupTextManager.Instance.ShowPopupNumber(transform.position, finalDamage, scale);
        //}
    }
}
