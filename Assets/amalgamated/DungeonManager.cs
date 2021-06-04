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
        if (isInDungeon)
        {
            LoadDungeonLevel();
        }
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
            GameManager.Instance.leaveBattle();

            Utils.setChildrenToInactive(PlantsManager.Instance. resourceParent);
        }
    }

    public void GetIntoDungeon()
    {
        currentLevel = 0;
        SceneManager.LoadScene(currentDungeonId);
        LoadDungeonLevel();
    }
    public void ClearPreviousLevel()
    {
        GameObject go = GameObject.Find("dungeonLevel");
        if (go)
        {
            Destroy(go);
        }
    }
    public void LoadDungeonLevel()
    {
        ClearPreviousLevel();
        GameObject parent = new GameObject("dungeonLevel");
        GameObject enemyParent = new GameObject("enemy");
        enemyParent.transform.parent = parent.transform;
        var levelInfo = JsonManager.Instance.getDungeonLevel(currentDungeonId, currentLevel);
        //for each enemy type, find all posible positions, put enemy there.
        if (levelInfo.enemyTypeCount != levelInfo.enemyCount.Count)
        {
            Debug.LogError("enemy count " + levelInfo.enemyCount.Count + " does not match enemy type " + levelInfo.enemyTypeCount);
            return;
        }
        Transform enemySpawnParent = GameObject.Find("enemySpawns").transform;
        for (int i = 0;i< levelInfo.enemyTypeCount; i++)
        {
            if (i >= levelInfo.enemies.Count)
            {
                Debug.LogError("too many enemy type, want " + i + " has " + levelInfo.enemies.Count);
                break;
            }
            GameObject prefab = Resources.Load<GameObject>("enemies/" + levelInfo.enemies[i]);

            Transform currentTypeSpawnParent = enemySpawnParent.Find(i.ToString());

            List<Transform> spawnTransforms= Utils.reservoirSamplingTransformChild(currentTypeSpawnParent, levelInfo.enemyCount[i]);
            foreach (Transform trans in spawnTransforms)
            {
                GameObject go = Instantiate(prefab, trans.position, Quaternion.identity, enemyParent.transform);
            }
        }

    }
    public void GetToNextDungeonLevel() {
        if (hasNextDungeonLevel())
        {

        }
        else
        {

        }
    }

    public bool hasNextDungeonLevel()
    {
        return true;
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
