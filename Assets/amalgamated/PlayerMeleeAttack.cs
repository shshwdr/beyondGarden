using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public float cooldownTime = 0.2f;
    float currentCooldownTimer;
    Collider2D collider;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        collider = GetComponent<Collider2D>();
    }

    public void setactive(bool isActive)
    {
        collider.enabled = isActive;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (FModSoundManager.Instance.isMerged)
        {
            return;
        }
        if (collision.collider.GetComponent<EnemyController>() )
        {
            if (currentCooldownTimer < cooldownTime)
            {
                return;
            }
            currentCooldownTimer = 0;
            collision.collider.GetComponent<EnemyController>().getDamage();

            playerController.attackAnim();
            AudioManager.Instance.playPlayerAttack();
            
        }

        if (collision.collider.GetComponent<BossController>())
        {
            if (currentCooldownTimer < cooldownTime)
            {
                return;
            }
            currentCooldownTimer = 0;
            collision.collider.GetComponent<BossController>().getDamage();

            playerController.attackAnim();
            AudioManager.Instance.playPlayerAttack();

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    // Update is called once per frame
    void Update()
    {

        currentCooldownTimer += Time.deltaTime;
        if (currentCooldownTimer >= 1.5 * cooldownTime)
        {
            playerController.stopAttackAnim();
        }
    }
}
