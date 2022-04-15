using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : FriendController
{
    public bool isAttachedToPlayer = false;
    public PlayerController playerController;

    public float stopAttachTimer = 0;
    bool isAttached;
    public bool canBeAttacked()
    {
        return !isDead && isAttachedToPlayer;
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
        if (stopAttachTimer > 0)
        {
            return;
        }
        if (isAttached)
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
        isAttached = true;
        Destroy(rb);
    }

    public void getDisembled()
    {
        isAttached = false;
        stopAttachTimer = playerController.stopAttachTime;
        transform.parent = playerController.transform.parent;
        //agent.enabled = true;

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = playerController.rb.gravityScale;
        rb.constraints = playerController.rb.constraints;
        rb.collisionDetectionMode = playerController.rb.collisionDetectionMode;
        rb.drag = 1f;
        //CopyComponent(playerController.rb, gameObject);

        Vector2 dir = transform.position - playerController.transform.position;
        dir.Normalize();

        rb.AddForce(dir* playerController.disembleForce,ForceMode2D.Impulse);


        isAttachedToPlayer = false;
        playerController = null;
    }

    protected override void Update()
    {
        base.Update();
        stopAttachTimer -= Time.deltaTime;
    }

    protected override void Die()
    {
        base.Die();
        playerController.allyCoundAdd(-1);

        GetComponent<AudioSource>().PlayOneShot(MusicManager.Instance.loseAlly);
        Destroy(gameObject, 0.5f);
    }
}



