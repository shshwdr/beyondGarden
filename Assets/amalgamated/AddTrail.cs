using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddTrail : MonoBehaviour
{
    public GameObject trail;
    public float destoryTrailTime = 5f;
    public float generateTrailCooldown = 0.5f;
   // public Transform trailGenerateTransform;
    float currentTrailTimer = 0;
    NavMeshAgent agent;
    HPCharacterController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyController = GetComponentInParent<HPCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FModSoundManager.Instance.isMerged && /*agent.velocity.magnitude >= 0.01*/!enemyController.isStuned)
        {
            if (currentTrailTimer >= generateTrailCooldown)
            {
                GameObject instance = Instantiate(trail, transform.position, Quaternion.identity);
                Destroy(instance, destoryTrailTime);
                currentTrailTimer -= generateTrailCooldown;
            }
            currentTrailTimer += Time.deltaTime;
        }
    }
}
