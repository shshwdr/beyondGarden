using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFlowerWeaponCell : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void UpdateInfo(string key)
    {
        image.sprite = JsonManager.Instance.getPlant(key).sprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
