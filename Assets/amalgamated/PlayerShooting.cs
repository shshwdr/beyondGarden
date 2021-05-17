using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float cooldownTime;
    float currentCooldownTimer;
    public GameObject hitEffect;
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
            GameObject hitEffectObject = Instantiate(hitEffect, mousePosition, Quaternion.identity);
            //aimAnimator.SetTrigger("Shoot");
            //GetComponents<AudioSource>()[0].PlayOneShot(shootClip);

            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, aimDirection,100, 1 << LayerMask.NameToLayer("Enemy"));
            if (raycastHit2D.collider != null)
            {
                EnemyController enemy = raycastHit2D.collider.GetComponent<EnemyController>();
                if (enemy)
                {
                    enemy.getDamage();



                    //if (enemy.GetComponent<AudioSource>())
                    //{

                    //    GetComponents<AudioSource>()[1].PlayOneShot(enemy.GetComponent<AudioSource>().clip);
                    //}

                }
            }

            //OnShoot?.Invoke(this, new OnShootEventArgs
            //{
            //    gunEndPointPosition = aimGunEndPointTransform.position,
            //    shootPosition = mousePosition,
            //    shellPosition = aimShellPositionTransform.position,
            //});
        }
    }
    }
