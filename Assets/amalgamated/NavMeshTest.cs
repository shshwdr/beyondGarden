using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    public float rebuildTime = 1;
    float currentRebuildTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRebuildTimer += Time.deltaTime;
        if (currentRebuildTimer > rebuildTime)
        {
            currentRebuildTimer = 0;
            GetComponent<NavMeshSurface2d>().BuildNavMesh();
        }

    }
}
