using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Sprite pressedDownImage;
    public Sprite normalImage;
    SpriteRenderer render;
    int pressedCount = 0;
    public RoomController room;
    public bool isDisembled = false;
    public GameObject UIWhenPress;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.sprite = normalImage;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDisembled && isPressed() && Input.GetKeyDown(KeyCode.Space))
        {
            var player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.disemble();
        }
    }

    public bool isPressed()
    {
        return pressedCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FriendController fc;
        if (isDisembled)
        {
            fc = collision.GetComponent<PlayerController>();
        }
        else
        {
            fc = collision.GetComponent<FriendController>();
        }
        if (fc)
        {

            pressedCount++;
            render.sprite = pressedDownImage;
            if (UIWhenPress)
            {
                UIWhenPress.SetActive(true);
            }
            if (pressedCount == 1)
            {
                if (room)
                {

                    room.getButtonPress();

                    if (GetComponent<AudioSource>())
                    {
                        GetComponent<AudioSource>().PlayOneShot(MusicManager.Instance.click);
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FriendController fc;
        if (isDisembled)
        {
            fc = collision.GetComponent<PlayerController>();
        }
        else
        {
            fc = collision.GetComponent<FriendController>();
        }
        if (fc)
        {
            pressedCount--;
            if (pressedCount == 0)
            {

                render.sprite = normalImage;
                if (UIWhenPress)
                {
                    UIWhenPress.SetActive(false);
                }
                if (room)
                {

                    room.getButtonRelease();
                }
            }
        }
    }

}
