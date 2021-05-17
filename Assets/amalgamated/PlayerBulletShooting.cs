using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletShooting : MonoBehaviour
{
    private float lastFire;
    public float fireDelay = 0.2f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    float lastShootx;
    float lastShooty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");
        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }
    }

    void Shoot(float x, float y)
    {
        

        lastShootx = x;
        lastShooty = y;

        shootBullet();
    }

    public void shootBullet()
    {

        float x = lastShootx;
        float y = lastShooty;
        float normalizedX = (x < 0) ? Mathf.Floor(x) : Mathf.Ceil(x);

        float normalizedY = (y < 0) ? Mathf.Floor(y) : Mathf.Ceil(y);

        Vector3 direction = new Vector3(0, -0.1f, 0);//new Vector3(normalizedX, normalizedY, 0) * 0.15f;
        //	RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2)(direction));

        //if (hit.collider != null)
        //{
        //	//it hit a wall, can't 
        //}
        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction, transform.rotation) as GameObject;
        //bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
        //bullet.GetComponent<BulletController>().shooter = this;
    }

}
