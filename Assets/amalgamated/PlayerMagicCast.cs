using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicCast : MonoBehaviour
{
    public float cooldownTime;
    float currentCooldownTimer;
    public GameObject hitEffect;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Utils.Pause)
        //{
        //    return;
        //}
        HandleShooting();
        currentCooldownTimer += Time.deltaTime;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (currentCooldownTimer < cooldownTime)
            {
                return;
            }
            currentCooldownTimer = 0;
            Vector3 mousePosition = GetMouseWorldPosition();
            //GameObject hitEffectObject = Instantiate(hitEffect, mousePosition, Quaternion.identity);
            //aimAnimator.SetTrigger("Shoot");
            //GetComponents<AudioSource>()[0].PlayOneShot(shootClip);

            Vector3 dir = (mousePosition - transform.position).normalized;

            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var quat = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, quat) as GameObject;
            //bullet.GetComponent<EnemyBullet>().GetPlayer(player.transform);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
            var weaponInfo = BattleManager.Instance.getCurrentWeapon();
            bullet.GetComponent<PlayerBulletController>().init(weaponInfo.attack, weaponInfo.weaponType);
        }
    }
    }
