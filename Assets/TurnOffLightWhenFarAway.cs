using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TurnOffLightWhenFarAway : MonoBehaviour
{
    GameObject player;
    float hideDistance = 100f;
    ShadowCaster2D shadow;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        hideDistance = player.GetComponent<PlayerController>().hideDistance;
        shadow = GetComponent<ShadowCaster2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).sqrMagnitude >= hideDistance)
        {
            shadow.enabled = false;
        }
        else
        {
            shadow.enabled = true;
        }
    }
}
