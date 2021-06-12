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
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isPressed()
    {
        return pressedCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FriendController>())
        {

            pressedCount++;
            render.sprite = pressedDownImage;
            if(pressedCount == 1)
            {
                room.getButtonPress();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<FriendController>())
        {
            pressedCount--;
            if (pressedCount == 0)
            {

                render.sprite = normalImage;
                room.getButtonRelease();
            }
        }
    }

}
