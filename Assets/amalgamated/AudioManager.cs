using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audio;
    public AudioClip[] playerAttackClips;
    public AudioClip[] bossDamage;
    public AudioClip bossDefeated;
    public AudioClip gameover;
    public AudioClip victory;
    public AudioClip merge;

    public AudioClip[] playerHurt;
    public AudioClip[] monsterDie;
    Dictionary<AudioClip, int> clipToId;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        clipToId = new Dictionary<AudioClip, int>();
    }

     void playMultipleClips(AudioClip[] clips)
    {
        var firstClip = clips[0];
        if (!clipToId.ContainsKey(firstClip))
        {
            clipToId[firstClip] = 0;
        }
        audio.PlayOneShot(clips[clipToId[firstClip]]);
        clipToId[firstClip]++;
        if (clipToId[firstClip] >= clips.Length)
        {
            clipToId[firstClip] = 0;
        }
    }

    public void playPlayerAttack()
    {
        playMultipleClips(playerAttackClips);
    }
    public void playPlayerHurt()
    {
        playMultipleClips(playerHurt);
    }
    public void playBossDamage(int stage)
    {
        playAudio(bossDamage[stage]);
    }
    public void playVicotry()
    {
        playAudio(victory);
    }
    public void playMonsterDie(int mergeLevel)
    {
        playAudio(monsterDie[mergeLevel]);
    }
    public void playGameOver()
    {
        playAudio(gameover);
    }
    public void playMerge()
    {
        playAudio(merge);
    }
    public void playBossDefeat()
    {
        playAudio(bossDefeated);
    }

    
    void playAudio(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
