using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestManager : Singleton<PestManager>
{
    [HideInInspector]
    public List<Pest> currentPests = new List<Pest>();
    public Transform pestSpawnParent;

    public Transform pestParent;

    public GameObject pestPrefab;

    float beeGenerateTime = -5;
    float currentBeeGenerateTime = 0;

    public bool lotsPest = false;

    public bool startGenerate;
    // Start is called before the first frame update

    public void Clear()
    {
        foreach(Transform t in pestParent)
        {
            Destroy(t.gameObject);
        }
        currentBeeGenerateTime = 0;
        beeGenerateTime = -1;
    }
    public List<Transform> pestsList()
    {
        List<Transform> res = new List<Transform>();
        foreach (var plantValue in currentPests)
        {
            res.Add(plantValue.transform);
        }
        return res;
    }
    void Start()
    {
        //StartCoroutine(spawnMultiWavePest(pestPrefab, 4, 1,1f));
    }

    IEnumerator spawnMultiWavePest(GameObject pest, float waveInterval, int summonAmount, float interval = 1f)
    {
        while(true)
        {
            yield return new WaitForSeconds(waveInterval);
            StartCoroutine(spawnMultiPest(pest, summonAmount, interval));
        }
    }

    IEnumerator spawnMultiPest(GameObject pest, int summonAmount, float interval = 1f)
    {
        for(int i = 0; i < summonAmount; i++)
        {
            spawnOnePest(pest);
            yield return new WaitForSeconds(interval);
        }
        yield return new WaitForSeconds(interval);
    }

    public void spawnOnePest(GameObject pest)
    {

        var trans = Utils.RandomTransform(pestSpawnParent);
        Instantiate(pest, trans);
    }

    public void AddPest(Pest p)
    {
        currentPests.Add(p);
    }

    public void updateGenerateTime()
    {
        var beeValue = PlantsManager.Instance.currentResource["bee"];
        if (beeValue <= 6)
        {
            beeGenerateTime = -1;
            return;
        }
        beeGenerateTime = Utils.SuperLerp(30, 5, 6, 40, beeValue);
    }
    // Update is called once per frame
    void Update()
    {
        //if (!startGenerate)
        //{
        //    return;
        //}
        if (lotsPest)
        {
            beeGenerateTime = 1;
        }
        if(beeGenerateTime > 0)
        {

            currentBeeGenerateTime += Time.deltaTime;
            if (currentBeeGenerateTime >= beeGenerateTime)
            {
                currentBeeGenerateTime = 0;
                var trans = Utils.RandomTransform(pestSpawnParent);
                Instantiate(pestPrefab, trans.position, Quaternion.identity, pestParent);

                StartCoroutine(delayShow());
            }
        }
    }

    IEnumerator delayShow()
    {
        yield return new WaitForSeconds(3);
        //TutorialManager.Instance.firstSeeSomething("pest");
    }
}
