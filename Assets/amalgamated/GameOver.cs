using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : Singleton<GameOver>
{
    public GameObject gameOverAllPanel;
    public GameObject gameOverPanel;
    public GameObject gameFinishPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Gameover()
    {
        gameOverAllPanel.SetActive(true);
        gameOverPanel.SetActive(true);
        gameFinishPanel.SetActive(false);
        GameManager.Instance.isGameOver = true;
        Time.timeScale = 0;

        DialogueManager.StopConversation();
    }

    public void GameFinish()
    {
        gameOverAllPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameFinishPanel.SetActive(true);
        GameManager.Instance.isGameOver = true;
        Time.timeScale = 0;

        DialogueManager.StopConversation();
    }

    public void Restart()
    {
        FModSoundManager.Instance.SetParam("Game Over", 0);
        FModSoundManager.Instance.startEvent("");
        DialogueLua.SetVariable("merged", false);
        DialogueLua.SetVariable("hasHelp", false);
        FModSoundManager.Instance.isMerged = false;
        gameOverAllPanel.SetActive(false);
        GameManager.Instance.GotoLevel(0);
        DialogueManager.StopConversation();
    }

    public void RestartLevel()
    {
        FModSoundManager.Instance.SetParam("Game Over", 0);

        FModSoundManager.Instance.startEvent("");
        gameOverAllPanel.SetActive(false);
        if(GameManager.Instance.currentLevel >= 6)
        {

            GameManager.Instance.GotoLevel(6);

            DialogueLua.SetVariable("merged", false);
            DialogueLua.SetVariable("hasHelp", false);
            FModSoundManager.Instance.isMerged = false;
        }
        else
        {
            GameManager.Instance.RestartLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
