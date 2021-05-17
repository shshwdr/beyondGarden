using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FModSoundManager : Singleton<FModSoundManager>
{
    float currentAmbienceIntensity;
    bool loaded = false;
    string currentEvent;
    FMOD.Studio.EventInstance[] ambiences = new FMOD.Studio.EventInstance[2];
    int currentId = 0;
    float volumnValue = 0.2f;
    public bool isMerged = false;
    public bool getHelpDialogue = false;
    float defaultVolumn = 0.2f;
    bool pressedStart = false;
    //[FMODUnity.EventRef]
    //public string eventName;
    // Start is called before the first frame update

    void Start()
    {
        //ambience = FMODUnity.RuntimeManager.CreateInstance(eventName);
        //ambience.start();
        // Invoke("delayTest", 0.1f);
        DontDestroyOnLoad(gameObject);
    }
    FMOD.Studio.EventInstance currentAmbience()
    {
        return ambiences[currentId];
    }

    public void resetVolumn()
    {
        SetVolumn(defaultVolumn);
    }
    public void startEvent(string eventName)
    {
        
        if(eventName == "")
        {

            currentEvent = eventName;
            return;
        }
        if (eventName != currentEvent)
        {
            Debug.Log("start even " + eventName);
           // if (currentAmbience() == null)
            {

                currentAmbience().stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            currentId++;
            if (currentId > 1)
            {
                currentId = 0;
            }
            ambiences[currentId].release();
            ambiences[currentId] = FMODUnity.RuntimeManager.CreateInstance(eventName);

            // ambience.setVolume(0.1f);
            currentAmbience().start();

            currentAmbience().setVolume(defaultVolumn);
            currentEvent = eventName;
        }
    }
    public void SetParam(string paramName, float value)
    {

        currentAmbience().setParameterByName(paramName, value);
    }
    public void SetVolumn(float value)
    {

        //transform.DOMove(new Vector3(2, 2, 2), 1);
﻿﻿﻿﻿﻿﻿﻿﻿// The generic way
//﻿﻿﻿﻿﻿﻿﻿﻿DOTween.To(() => transform.position, x => transform.position = x, new Vector3(2, 2, 2), 1);
        currentAmbience().setVolume(value);
    }
    public void SetAmbienceParamter(float param)
    {
        //if (currentAmbienceIntensity != param)
        {
            print("set ambience to " + param);
            currentAmbienceIntensity = param;
            currentAmbience().setParameterByName("Intensity", param);
            //ambience.setParameterByName()
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master") && !loaded)
        {
            loaded = true;
            Debug.Log("Master Bank Loaded");
            startEvent("event:/Town - Forest");
            //SceneManager.LoadScene(1);
        }
        if(pressedStart && loaded)
        {
            pressedStart = false;

            SceneManager.LoadScene(1);
        }
    }
    private void OnDestroy()
    {
        currentAmbience().stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //Destroy(ambience);
    }
    public void startGame()
    {
        pressedStart = true;
    }
}
