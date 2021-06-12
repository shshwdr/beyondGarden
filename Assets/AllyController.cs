using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : FriendController
{
    public bool isAttachedToPlayer = false;
    public PlayerController playerController;
    UnityEngine.AI.NavMeshAgent agent;

    public float stopAttachTimer = 0;
    bool isAttached;
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
        //rb.simulated = false;
        //rb.bodyType = RigidbodyType2D.Kinematic;
        //agent.enabled = false;
    }

    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
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
        Destroy(gameObject, 0.3f);
    }
}



