using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSprite : MonoBehaviour
{
    float dashTime;
    float currentTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init(Sprite s,Color c,float t)
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = s;
        sprite.color = c;
        dashTime = t;
    }
    public void Play(Transform t)
    {
        transform.position = t.position;
        //transform.localScale = t.localScale;
        gameObject.SetActive(true);
        currentTimer = dashTime;
    }
    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
