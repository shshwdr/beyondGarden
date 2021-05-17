using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeObstacles : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D collider;
    public void getCollide(Vector3 collideForce)
    {
        gameObject.SetActive(false);
        //collider.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
