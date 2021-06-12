using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioClip playerClip;
    public List<AudioClip> allyClips;
    public List<AudioClip> enemyClips;

    public List<AudioSource> allySources;
    public List<AudioSource> enemySources;

    public List<int> allyCountChange = new List<int>()
    {
        1,4,6
    };

    // Start is called before the first frame update
    void Start()
    {
        var go = gameObject.AddComponent<AudioSource>();
        go.clip = playerClip;
        go.loop = true;
        go.Play();
        for (int i = 0; i < allyClips.Count; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = allyClips[i];
            allySources.Add(source);
            source.loop = true;
        }
        for (int i = 0; i < enemyClips.Count; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = enemyClips[i];
            enemySources.Add(source);
            source.loop = true;
        }
    }

    public void allyChanged(int count)
    {
        int i = 0;
        for (;i< allyClips.Count; i++)
        {
            if (count >= allyCountChange[i])
            {
                allySources[i].Play();
            }
            else
            {
                break;
            }
        }
        for(;i< allySources.Count; i++)
        {
            allySources[i].Stop();
        }
    }

    public void startEnemySound(int i)
    {
        if (i < 0)
        {
            return;
        }
        enemySources[i].Play();
    }
    public void stopEnemySound(int i)
    {
        if (i < 0)
        {
            return;
        }
        enemySources[i].Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
