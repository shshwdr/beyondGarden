using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPair : MonoBehaviour
{
    public TMP_Text valueText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateValue(string v)
    {
        valueText.text = v;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
