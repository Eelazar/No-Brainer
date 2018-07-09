using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    
    public Toggle devMode;

    private Button[] menuButtons;
    private bool devSwitchBool;
    
	void Start ()
    {
        menuButtons = this.GetComponentsInChildren<Button>();
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i == 0) menuButtons[i].gameObject.SetActive(true);
            else menuButtons[i].gameObject.SetActive(false);
        }

        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("ScenePreview", "TutorialPreview"), LoadSceneMode.Additive);
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
