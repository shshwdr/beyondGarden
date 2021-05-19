using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : Singleton<BirdManager>
{
    public GameObject bird;
    Vector3 originPosition;
    AudioSource audiosource;


    public float birdShowtimeMin = 5f;
    public float birdShowtimeMax = 10f;

    public Transform spawnBirdHighest;
    public Transform spawnBirdLowest;
    float birdShowTimer = -1;
    float birdShowCurrentTime;
    public bool autoResrouceStart;

    //Dictionary<HelperPlantType, HelperPlantType> treeToUnlockFlower = new Dictionary<HelperPlantType, HelperPlantType>()
    //{
    //    {HelperPlantType.appleTree2, HelperPlantType.stawberry },

    //    {HelperPlantType.appleTree3, HelperPlantType.zinnia },
    //};

    //public Dictionary<HelperPlantType, bool> needToUnlock = new Dictionary<HelperPlantType, bool>();
    // Start is called before the first frame update
    void Start()
    {
        originPosition = bird.transform.position;
        updateShowTimer();
        audiosource = GetComponent<AudioSource>();
    }
    void updateShowTimer()
    {
        birdShowTimer = Random.Range(birdShowtimeMin, birdShowtimeMax);
    }

    public void ResetBird()
    {
        birdShowCurrentTime = 0;
        bird.transform.position = originPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (!autoResrouceStart)
        {
            return;
        }
        birdShowCurrentTime += Time.deltaTime;
        if (birdShowCurrentTime >= birdShowTimer)
        {
            updateShowTimer();
            Vector3 position = new Vector3(spawnBirdHighest.position.x, Random.Range(spawnBirdLowest.position.y, spawnBirdHighest.position.y), -1f);
            //Instantiate(bird, position,Quaternion.identity);
            bird.transform.position = position;
            bird.GetComponent<Bird>().isClicked = false;
            bird.GetComponent<Bird>().Appear();
            birdShowCurrentTime = 0;
            StartCoroutine(delayShow());
        }
    }

    IEnumerator delayShow()
    {
        yield return new WaitForSeconds(1);
        //TutorialManager.Instance.firstSeeSomething("bird");
    }

    public void startTreePlant(string type)
    {
        //if(treeToUnlockFlower.ContainsKey(type) && !needToUnlock.ContainsKey(type))
        //{
        //    needToUnlock[treeToUnlockFlower[type]] = true;
        //}
    }

    public void updateDropbox(Dropbox box)
    {
        //foreach (var key in needToUnlock.Keys)
        //{
        //    if (needToUnlock[key] == true)
        //    {
        //        //needToUnlock[key] = false;
        //        box.dropboxType = DropboxType.unlock;
        //        box.unlockPlant = key;
        //        return;
        //    }
        //}

        //box.dropboxType = DropboxType.resource;

        //PlantProperty[] dropProperties = new PlantProperty[]{
        //    PlantProperty.n,  PlantProperty.p, PlantProperty.water,
        //};
        //var typeRandom = Random.Range(0, 3);

        //box.resource = new Dictionary<PlantProperty, int>() {
        //    {dropProperties[typeRandom], Random.Range(5, 10) },
        //}; 
    }
}
