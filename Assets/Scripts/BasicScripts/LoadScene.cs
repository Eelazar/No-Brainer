using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string sceneToLoad;
    public bool mustInteract;

    private Button btn;

    private bool interacted;


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
	void Update ()
    {
	    if(Input.GetButton("Fire1") && interacted)
        {
            LoadNewScene();
        }	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (mustInteract)
        {
            interacted = true;
        }
        else
        {

            LoadNewScene();
        }
    }

    void LoadNewScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
    }
}
