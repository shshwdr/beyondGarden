using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLevelManager : MonoBehaviour
{
    DungeonManager dm;
    // Start is called before the first frame update
    void Start()
    {
        dm = DungeonManager.Instance;
        LoadDungeonLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDungeonLevel()
    {
        //ClearPreviousLevel();
        GameObject parent = new GameObject("dungeonLevel");
        GameObject enemyParent = new GameObject("enemy");
        enemyParent.transform.parent = parent.transform;
        var levelInfo = JsonManager.Instance.getDungeonLevel(dm.currentDungeonId, dm.currentLevel);
        //for each enemy type, find all posible positions, put enemy there.
        if (levelInfo.enemyTypeCount != levelInfo.enemyCount.Count)
        {
            Debug.LogError("enemy count " + levelInfo.enemyCount.Count + " does not match enemy type " + levelInfo.enemyTypeCount);
            return;
        }
        Transform enemySpawnParent = GameObject.Find("enemySpawns").transform;
        for (int i = 0; i < levelInfo.enemyTypeCount; i++)
        {
            if (i >= levelInfo.enemies.Count)
            {
                Debug.LogError("too many enemy type, want " + i + " has " + levelInfo.enemies.Count);
                break;
            }
            GameObject prefab = Resources.Load<GameObject>("enemies/" + levelInfo.enemies[i]);

            Transform currentTypeSpawnParent = enemySpawnParent.Find(i.ToString());

            List<Transform> spawnTransforms = Utils.reservoirSamplingTransformChild(currentTypeSpawnParent, levelInfo.enemyCount[i]);
            foreach (Transform trans in spawnTransforms)
            {
                GameObject go = Instantiate(prefab, trans.position, Quaternion.identity, enemyParent.transform);
            }
        }

    }
}
