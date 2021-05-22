using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pest : Animal
{
    public AudioClip clickSound;
    // Start is called before the first frame update
    protected override void Start()
    {
        PestManager.Instance.AddPest(this);
        base.Start();
    }
    private void OnMouseDown()
    {
        //find closest helperInsect
        //no target
        Transform targeter = null;
        var helperInsectsNoTarget = HelperInsectManager.Instance.helperInsectsNoTargetList();
        if (helperInsectsNoTarget.Count > 0)
        {
            targeter = getClosestTransform(helperInsectsNoTarget);
        }
        else
        {
            var helperInsects = HelperInsectManager.Instance.helperInsectsList();
            if (helperInsects.Count > 0)
            {
                targeter = getClosestTransform(helperInsects);
            }
        }
        if (targeter)
        {
            targeter.GetComponent<HelperInsect>().target = transform;

            audiosource.PlayOneShot(clickSound);
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!target)
        {
            target = getClosestTransform(MainGameManager.Instance.plantsList());
        }
        base.Update();
    }
}
