using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
    
	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
        if(PlayerPrefs.GetString("Scene") == "Menu")
        {
            SceneManager.LoadSceneAsync(PlayerPrefs.GetString("ScenePreview", "TutorialPreview"), LoadSceneMode.Additive);
        }
        

    }

    // Update is called once per frame
    void Update ()
    {
    }
    
}
