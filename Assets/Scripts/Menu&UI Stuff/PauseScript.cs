using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    private bool paused;
    private Button menuButton;
    private Button restartButton;
    private Transform child;

    void Start()
    {
        child = transform.GetChild(0);
        child.gameObject.SetActive(false);

        if(child.GetComponentsInChildren<Button>().Length >= 2)
        {
            menuButton    = child.GetComponentsInChildren<Button>()[0];
            restartButton = child.GetComponentsInChildren<Button>()[1];

            menuButton.onClick.AddListener(LoadMenu);
            restartButton.onClick.AddListener(RestartLvl);
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

        child.gameObject.SetActive(false);
    }
}
