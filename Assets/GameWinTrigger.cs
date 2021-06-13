using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinTrigger : MonoBehaviour
{
    public GameObject WinUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FriendController>())
        {

            WinUI.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
