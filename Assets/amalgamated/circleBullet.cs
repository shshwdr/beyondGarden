using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleBullet : MonoBehaviour
{
    public float rotateSpeed = 100;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().getDamage();
        }
    }
}
