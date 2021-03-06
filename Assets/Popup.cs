using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Popup : Singleton<Popup>
{

    public TMP_Text text;
    public Button yesButton;

    public Button noButton;

    public CanvasGroup group;
    public float duration = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init(string t, Action y)
    {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
        text.text = t;

        HUD.Instance.togglePause();
        clearButton();
        yesButton.onClick.AddListener(delegate {
            Hide(); y(); 
        });
        noButton.onClick.AddListener(delegate { Hide();  });
    }

    void clearButton()
    {

        yesButton.onClick.RemoveAllListeners();

        noButton.onClick.RemoveAllListeners();
    }



    public void Hide()
    {
        group.alpha = 0;
        HUD.Instance.togglePause();
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
