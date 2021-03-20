﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInfo : MonoBehaviour
{

    public TMP_Text levelInfoText;
    PlantsManager plantManager;
    // Start is called before the first frame update
    void Start()
    {
        plantManager = PlantsManager.Instance;
    }

    public void UpdateInfo(HelperPlantType treeType)
    {
        levelInfoText.text = "This is a " + plantManager.plantName[treeType];

        levelInfoText.text += "\n";

        levelInfoText.text += plantManager.levelDetail[treeType];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
