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
        if(this.GetComponent<Button>() != null)
        {
            btn = this.GetComponent<Button>();
            btn.onClick.AddListener(LoadNewScene);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        LoadNewScene();
    }

    void LoadNewScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
    }
}
