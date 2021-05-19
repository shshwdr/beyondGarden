using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TutorialPanel : Singleton<TutorialPanel>
{

    //public TMP_Text text;
    //public Button yesButton;


    //public CanvasGroup group;
    //public float duration = 0.3f;
    //bool isShowing;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}
    //public void Init(string t, Action y = null)
    //{
    //    group.alpha = 1;

    //    group.interactable = true;
    //    group.blocksRaycasts = true;
    //    text.text = t;
    //    clearButton();
    //    yesButton.onClick.AddListener(delegate { 
    //        if(y!=null)
    //            y();
    //        Hide();
    //        TutorialManager.Instance.finishPopup(t);
    //    });
    //    HUD.Instance.togglePause();
    //    isShowing = true;
    //    if (TutorialManager.Instance.skipTutorial)
    //    {
    //        Hide();
    //        TutorialManager.Instance.finishPopup(t);
    //    }
    //}

    //public bool canGeneratePanel()
    //{
    //    return !isShowing;
    //}

    //void clearButton()
    //{

    //    yesButton.onClick.RemoveAllListeners();
        
    //}



    //public void Hide()
    //{
    //    group.alpha = 0;
    //    HUD.Instance.togglePause();
    //    isShowing = false;

    //    group.interactable = false;
    //    group.blocksRaycasts = false;
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //}
}
