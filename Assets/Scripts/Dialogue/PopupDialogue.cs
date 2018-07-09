using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupDialogue : MonoBehaviour {


    //References
    private Text popupTextBox;
    private Image background;
    
	void Start ()
    {
        background = gameObject.GetComponentInChildren<Image>();
        background.enabled = false;
        popupTextBox = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Write(string text)
    {
        background.enabled = true;
        popupTextBox.text = text;
    }

    public void Clear()
    {
        background.enabled = false;
        popupTextBox.text = "";
    }
}
