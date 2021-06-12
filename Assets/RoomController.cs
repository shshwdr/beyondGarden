using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    Collider2D roomCollider;
    public Transform enemies;
    public Transform doors;
    public Transform showWhenFinish;
    public Transform hideWhenFinish;
    int enemyCount = 0;
    public bool isRoomActive;
    // Start is called before the first frame update
    void Awake()
    {
        roomCollider = GetComponent<Collider2D>();

    }
    private void Start()
    {
        foreach (Transform t in enemies)
        {
            enemyCount++;
            t.GetComponent<EnemyController>().addRoom(this);
        }
    }

    public void getEnemyDie()
    {
        enemyCount -= 1;
        if(enemyCount == 0)
        {
            finishRoom();
        }
    }

    public void finishRoom()
    {
        if (doors)
        {

            doors.gameObject.SetActive(false);
        }
        if (hideWhenFinish)
        {

            hideWhenFinish.gameObject.SetActive(false);
        }
        if (showWhenFinish)
        {
            showWhenFinish.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            isRoomActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            isRoomActive = false;
        }
    }
}
