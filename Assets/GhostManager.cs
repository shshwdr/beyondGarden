using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public Sprite ghostSprite;
    int ghostCount;
    public float dashTime;
    public float ghostStayTime = 0.1f;
    public Color color;
    public Transform characterT;
    float timer = -1;
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        ghostCount = transform.childCount;
        foreach(Transform t in transform)
        {
            t.GetComponent<GhostSprite>().Init(ghostSprite, color, ghostStayTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {

            var t = dashTime / (float)ghostCount;
            timer += Time.deltaTime;
            if (timer >= currentIndex * t)
            {
                generateGhost(currentIndex);
                currentIndex++;
            }
            if(currentIndex >= ghostCount)
            {
                timer = -1;
                currentIndex = 0;
            }
        }

    }

    public void StartGhost()
    {
        timer = 0;
        //for(int i = 0; i < ghostCount; i++)
        //{
        //    StartCoroutine( generateGhost(i));

        //}
    }

    void generateGhost(int index)
    {
        var t = dashTime / (float)ghostCount;
        Debug.Log("time " + t + " position " + characterT.position);
        transform.GetChild(index).GetComponent<GhostSprite>().Play(characterT);
       // yield return new WaitForSeconds(t);
    }
}
