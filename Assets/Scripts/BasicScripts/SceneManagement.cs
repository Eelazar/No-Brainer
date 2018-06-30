using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
    
	// Use this for initialization
	void Start ()
    {
        SceneManager.LoadSceneAsync(PlayerPrefs.GetString("CurrentScene", "TutorialPreview"), LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update ()
    {
    }
    
}
