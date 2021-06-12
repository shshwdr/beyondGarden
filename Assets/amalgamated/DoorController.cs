using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool needKey;
    bool isOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if(player && player.inventory.ContainsKey("key") && player.inventory["key"] > 0)
        {
            open();
        }
    }
    public void open()
    {
        if (isOpened)
        {
            return;
        }
        isOpened = true;
        GetComponentInChildren<Animator>().SetTrigger("open");
        GetComponent<BoxCollider2D>().enabled = false;
    }

    //public string nextLevelName;
    //public GameObject openedDoor;
    //public GameObject closedDoor;
    //public bool isOpened = false;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (DialogueEventHelper.Instance.dialogueFinished && !isOpened)
    //    {
    //        if (EnemyManager.instance.isLevelCleared)
    //        {
    //            openedDoor.SetActive(true);
    //            closedDoor.SetActive(false);
    //            isOpened = true;
    //            if(DungeonManager.Instance.currentLevel == 0)
    //            {

    //            }
    //            else
    //            {
    //                FModSoundManager.Instance.SetVolumn(0);
    //                AudioManager.Instance.playVicotry();
    //                Invoke("restartSound", 0.8f);
    //                //AudioManager.Instance.playVicotry();
    //                //FModSoundManager.Instance.SetParam("Victory", 1);
    //                //Invoke("resetSound", 0.8f);
    //            }
    //        }
    //    }
    //}

    //void resetSound()
    //{
    //    FModSoundManager.Instance.SetVolumn(0);
    //    AudioManager.Instance.playVicotry();
    //    Invoke("restartSound",0.8f);
    //    //FModSoundManager.Instance.SetParam("Victory", 0);
    //}
    //void restartSound()
    //{
    //    FModSoundManager.Instance.resetVolumn();
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (isOpened)
    //    {

    //        if (collision.collider.GetComponent<PlayerMeleeAttack>())
    //        {
    //            //GameManager.Instance.GoToNextLevel();
    //            // DungeonManager.Instance.GoToLevel(nextLevelName);
    //            DungeonManager.Instance.GetToNextDungeonLevel();
    //        }
    //    }
    //}
}
