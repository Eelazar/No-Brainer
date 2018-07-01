using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    
    public Toggle devMode;

    private Button[] menuButtons;
    private bool devSwitchBool;

	// Use this for initialization
	void Start ()
    {
        menuButtons = this.GetComponentsInChildren<Button>();
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i == 0) menuButtons[i].gameObject.SetActive(true);
            else menuButtons[i].gameObject.SetActive(false);
        }
        Debug.Log(menuButtons.Length);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (devMode.isOn)
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                if (i == 0) menuButtons[i].gameObject.SetActive(true);
                else menuButtons[i].gameObject.SetActive(false);
            }
        }

    }
    

    void SwitchMenuMode(bool on)
    {
        
    }
}
