using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FModParamChangeTrigger : MonoBehaviour
{
    public string paramName;
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FModSoundManager.Instance.SetParam(paramName, value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
