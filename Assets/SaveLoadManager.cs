using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public void saveGame()
    {
        // 1
        //Save save = CreateSaveGameObject();
        CSSerializedObject save = PlantsManager.Instance.Save();
        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);


        file.Close();


        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(Application.persistentDataPath + "/saveload.json", json);

        Debug.Log("Game Saved");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)){
            saveGame();
        }
    }
}
