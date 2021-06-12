using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : FriendController
{
    public bool isAttachedToPlayer = false;
    public PlayerController playerController;
    UnityEngine.AI.NavMeshAgent agent;
    public bool canBeAttacked()
    {
        return !isDead && isAttachedToPlayer;
    }
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    public override void getDamage(float damage = 1, string element = "")
    {
        if (isAttachedToPlayer)
        {
            base.getDamage(damage, element);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.getAlly(this);
        }
        else
        {

            var ally = collision.gameObject.GetComponent<AllyController>();
            if (ally && !ally.isDead && ally.isAttachedToPlayer)
            {
                ally.playerController.getAlly(this);
            }
        }
    }

    public void getAttached()
    {
        //rb.bodyType = RigidbodyType2D.Kinematic;
        agent.enabled = false;
    }

    protected override void Die()
    {
        base.Die();

        Destroy(gameObject, 0.3f);
    }
}



