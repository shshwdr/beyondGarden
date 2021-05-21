using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    bool inBattle;
    public bool isInBattle { get { return inBattle; } }
    public void getIntoBattle()
    {
        inBattle = true;
    }
    public void leaveBattle()
    {
        inBattle = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
