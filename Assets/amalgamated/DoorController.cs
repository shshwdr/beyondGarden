using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject openedDoor;
    public GameObject closedDoor;
    public bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueEventHelper.Instance.dialogueFinished && !isOpened)
        {
            if (EnemyManager.instance.isLevelCleared)
            {
                openedDoor.SetActive(true);
                closedDoor.SetActive(false);
                isOpened = true;
                if(GameManager.Instance.currentLevel == 0)
                {

                }
                else
                {
                    FModSoundManager.Instance.SetVolumn(0);
                    AudioManager.Instance.playVicotry();
                    Invoke("restartSound", 0.8f);
                    //AudioManager.Instance.playVicotry();
                    //FModSoundManager.Instance.SetParam("Victory", 1);
                    //Invoke("resetSound", 0.8f);
                }
            }
        }
    }

    void resetSound()
    {
        FModSoundManager.Instance.SetVolumn(0);
        AudioManager.Instance.playVicotry();
        Invoke("restartSound",0.8f);
        //FModSoundManager.Instance.SetParam("Victory", 0);
    }
    void restartSound()
    {
        FModSoundManager.Instance.resetVolumn();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpened)
        {

            if (collision.collider.GetComponent<PlayerMeleeAttack>())
            {
                GameManager.Instance.GoToNextLevel();
            }
        }
    }
}
