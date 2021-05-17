using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Demons, Undeads, Mushroom};
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;
    public PlayerController player;
    public int enemyMaxLevel = 3;

    public Dictionary<EnemyType, List<EnemyController>> enemiesDictionary;
    public bool isLevelCleared = false;

    public GameObject[] minionsToSpawn;
    public BossController bossController;


    public void spawnMinions(Vector3 position)
    {
        var minionToSpawn = minionsToSpawn[Random.Range(0, 3)];
        Instantiate(minionToSpawn, position, Quaternion.identity);
    }
    public void updateLevel()
    {
        isLevelCleared = true;
        foreach (var enemyList in enemiesDictionary.Values)
        {
            foreach (var enemy in enemyList)
            {
                if (enemy && !enemy.isDead )
                {
                    isLevelCleared = false;
                }
            }
        }
        if (bossController && !bossController.isReallyDead())
        {

            isLevelCleared = false;
        }
    }
    public void updateEnemies()
    {
        float highestLevel = -1;
        foreach(var enemyList in enemiesDictionary.Values)
        {
            foreach(var enemy in enemyList)
            {
                if (enemy &&!enemy.isDead&& enemy.mergeLevel > highestLevel)
                {
                    highestLevel = enemy.mergeLevel;
                }
            }
        }
        highestLevel += 1f;
        Debug.Log("highest level "+ highestLevel);
        FModSoundManager.Instance.SetAmbienceParamter(highestLevel);
        updateLevel();
    }

    private void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            instance.gameObject.transform.position = transform.position;
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }


        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        enemiesDictionary = new Dictionary<EnemyType, List<EnemyController>>();
        foreach (EnemyType t in System.Enum.GetValues(typeof(EnemyType)))
        {
            enemiesDictionary[t] = new List<EnemyController>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void destoryAllEnemies()
    {
        foreach (var enemyList in enemiesDictionary.Values)
        {
            foreach (var enemy in enemyList)
            {
                if (enemy && !enemy.isDead)
                {
                    enemy.getDamage(10000);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            destoryAllEnemies();
            if (bossController)
            {

                bossController.getDamage(10000);
            }

        }
    }
}
