using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagersBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void allLeave()
    {
        foreach(var child in transform.GetComponentsInChildren<VillagerBehavior>())
        {
            child.leave(); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
