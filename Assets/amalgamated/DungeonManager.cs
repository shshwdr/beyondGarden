using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : Singleton<DungeonManager>
{
    bool isCheatOn = true;
    public int currentLevel;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GoToNextLevel()
    {
        currentLevel += 1;
        GotoLevel(currentLevel);
    }
    public void GoToLevel(string levelName)
    {
        if(levelName == "")
        {
            //finished a level, get back
            SceneManager.LoadScene(0);
        }
    }
    public void GotoLevel(int level)
    {
        currentLevel = level;
        //hideRestartButton();
        Time.timeScale = 1;
        isGameOver = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(level+1);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            GoToLevel("");
        }
        if (isGameOver)
        {
            Time.timeScale = 0;
        }
        for (int i = 0; i < 9; i++)
        {
            if (isCheatOn && Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                GotoLevel(i);

                //SceneManager.LoadScene(i);
            }
        }
        //if ((!EnemyManager.instance.player || EnemyManager.instance.player.isDead) && Input.GetKeyDown(KeyCode.R))
        //{
        //    RestartLevel();
        //}
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        isGameOver = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
