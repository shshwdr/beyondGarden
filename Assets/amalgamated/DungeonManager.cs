using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : Singleton<DungeonManager>
{
    bool isCheatOn = true;
    public int currentLevel;
    public bool isGameOver;

    public string currentDungeonId = "dungeon1";
    public bool isInDungeon = false;

    // Start is called before the first frame update
    void Start()
    {
        //if (isInDungeon)
        //{
        //    LoadDungeonLevel();
        //}
        DontDestroyOnLoad(gameObject);
    }

    //public void GoToNextLevel()
    //{
    //    currentLevel += 1;
    //    GotoLevel(currentLevel);
    //}
    //public void GoToLevel(string levelName)
    //{
    //    if(levelName == "")
    //    {
    //        //finished a level, get back
    //        SceneManager.LoadScene(0);
    //        GameManager.Instance.leaveBattle();

    //        Utils.setChildrenToInactive(PlantsManager.Instance. resourceParent);
    //    }
    //}

    public void GetIntoDungeon()
    {
        currentLevel = 0;
        BattleManager.Instance.initBattleManager();
        SceneManager.LoadScene(currentDungeonId);
        //LoadDungeonLevel();
    }
    public void ClearPreviousLevel()
    {
        GameObject go = GameObject.Find("dungeonLevel");
        if (go)
        {
            Destroy(go);
        }
    }
    
    public void GetToNextDungeonLevel() {
        currentLevel++;
        if (hasNextDungeonLevel())
        {
            SceneManager.LoadScene(currentDungeonId);
            //LoadDungeonLevel();
        }
        else
        {
            //win dungeon
            LeaveDungeon();
        }
    }

    public void LeaveDungeon()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.leaveBattle();

        Utils.setChildrenToInactive(PlantsManager.Instance.resourceParent);
    }

    public bool hasNextDungeonLevel()
    {
        return currentLevel<JsonManager.Instance.getDungeonLevels(currentDungeonId).Count;
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            //kill all enemies

            GameObject enemyParent = GameObject.Find("enemy");
            foreach(Transform child in enemyParent.transform)
            {
                var e = child.GetComponent<EnemyController>();
                e.getDamage(10000);
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LeaveDungeon();
        }
        if (isGameOver)
        {
            Time.timeScale = 0;
        }
        for (int i = 0; i < 9; i++)
        {
            //if (isCheatOn && Input.GetKeyDown(KeyCode.Alpha1 + i))
            //{
            //    GotoLevel(i);

            //    //SceneManager.LoadScene(i);
            //}
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
