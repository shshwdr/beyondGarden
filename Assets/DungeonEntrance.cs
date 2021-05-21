using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrance : MonoBehaviour
{
    WeaponSelection weaponSelection;
    // Start is called before the first frame update
    void Start()
    {
        weaponSelection = GameObject.FindObjectOfType<WeaponSelection>();
        weaponSelection.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        weaponSelection.showMenu();
    }

}
