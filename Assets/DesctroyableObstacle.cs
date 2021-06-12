using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesctroyableObstacle : HPCharacterController
{
    protected override void Die()
    {
        //spawn drops if lucky!
        //always generate items
        //generate seed

        //generateDrop();


        base.Die();
        animator.SetTrigger("open");
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
        //animator.SetTrigger("die");
        // AudioManager.Instance.playMonsterDie(mergeLevel);
        //deathAnimator.enabled = true;
        //Destroy(gameObject, 0f);
    }
}
