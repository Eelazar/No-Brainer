using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
    
	void Start ()
    {
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString("ScenePreview", GetBaseSceneName() + "Preview");    
    }
    
    void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
        //}
    }

    string GetBaseSceneName()
    {
        string name = SceneManager.GetActiveScene().name;
        string[] split = name.Split('_');

        name = split[0];
        return name;
    }
    
}
