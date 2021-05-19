using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OneStatHud : MonoBehaviour
{
    public TMP_Text name;
    public Image image;

    public TMP_Text value;
    public TMP_Text rate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(string key, float v)
    {
        CurrencyInfo info = JsonManager.Instance.getCurrency(key);
        name.text = info.name;
        image.sprite = info.sprite;
        value.text = v.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
