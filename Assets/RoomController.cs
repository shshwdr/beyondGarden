using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    Collider2D roomCollider;
    public Transform enemies;
    public Transform buttons;
    public List<Transform> doors;
    public Transform showWhenFinish;
    public Transform hideWhenFinish;
    int enemyCount = 0;
    int buttonCount = 0;
    public bool isRoomActive;
    bool isFinished;
    public int roomMusicId = 0;
    public GameObject hiddenRoom;
    // Start is called before the first frame update
    void Awake()
    {
        roomCollider = GetComponent<Collider2D>();
        if (hiddenRoom)
        {
            hiddenRoom.SetActive(false);
        }

    }
    private void Start()
    {
        if (enemies)
        {

            foreach (Transform t in enemies)
            {
                enemyCount++;
                t.GetComponent<EnemyController>().addRoom(this);
            }
        }
        if (buttons)
        {
            foreach (Transform t in buttons)
            {
                if (t.gameObject.active)
                {

                    buttonCount++;
                    t.GetComponent<ButtonTrigger>().room = this;
                }
            }
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

    public void getButtonPress()
    {
        buttonCount -= 1;

        if (buttonCount == 0)
        {
            finishRoom();
        }
    }

    public void getButtonRelease()
    {
        buttonCount += 1;
    }

    public void finishRoom()
    {
        if (isFinished)
        {
            return;
        }
        isFinished = true;
        MusicManager.Instance.stopEnemySound(roomMusicId);
        if (doors.Count>0)
        {
            foreach (var ds in doors)
            {
                foreach(Transform door in ds)
                {

                    //door.gameObject.SetActive(false);
                    door.GetComponent<DoorController>().open();
                }
            }
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
        if (collision.gameObject.GetComponent<PlayerController>() && !isFinished)
        {
            isRoomActive = true;
            MusicManager.Instance.startEnemySound(roomMusicId);
            if (hiddenRoom)
            {

                hiddenRoom.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            isRoomActive = false;
            MusicManager.Instance.stopEnemySound(roomMusicId);
            if (hiddenRoom)
            {

                hiddenRoom.SetActive(false);
            }
        }
    }
}
