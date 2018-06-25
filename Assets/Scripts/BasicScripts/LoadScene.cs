using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string sceneToLoad;

    private Button btn;

    // Use this for initialization
    void Start ()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(LoadNewScene);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadNewScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
