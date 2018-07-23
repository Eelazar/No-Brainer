using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
      
    private bool paused, controlsOn;
    private Button menuButton;
    private Button restartButton;
    private Button controlsButton;
    private Transform child;
    private Transform controlsImage;

    void Start()
    {
        child = transform.GetChild(0);
        child.gameObject.SetActive(false);
        controlsImage = transform.GetChild(1);
        controlsImage.gameObject.SetActive(false);

        if(child.GetComponentsInChildren<Button>().Length >= 3)
        {
            menuButton     = child.GetComponentsInChildren<Button>()[0];
            restartButton  = child.GetComponentsInChildren<Button>()[1];
            controlsButton = child.GetComponentsInChildren<Button>()[2];

            menuButton.onClick.AddListener(LoadMenu);
            restartButton.onClick.AddListener(RestartLvl);
            controlsButton.onClick.AddListener(ShowControls);
        }
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            UnPause();
        }
        if (Input.GetButtonDown("Fire1") && controlsOn)
        {
            controlsImage.gameObject.SetActive(false);
            controlsOn = false;
        }
    }

    void LoadMenu()
    {
        UnPause();
        SceneManager.LoadScene("Menu");
    }

    void RestartLvl()
    {
        UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowControls()
    {
        controlsImage.gameObject.SetActive(true);
        controlsOn = true;
    }

    void Pause()
    {
        Time.timeScale = 0;
        paused = true;

        child.gameObject.SetActive(true);
    }

    void UnPause()
    {
        Time.timeScale = 1;
        paused = false;
        controlsImage.gameObject.SetActive(false);
        controlsOn = false;

        child.gameObject.SetActive(false);
    }
}
