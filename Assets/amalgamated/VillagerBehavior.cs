using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class VillagerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leave()
    {
        var tween = transform.DOMoveY(transform.position.y -  5, 4);
        tween.SetUpdate(true);
        Destroy(gameObject, 4);
    }
}
