using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string sceneToLoad;
    public Vector3 newSpawnPoint;
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
        if(other.tag == "Player")
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
    }

    void LoadNewScene()
    {
        SetSpawn(newSpawnPoint);
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
    }

    void SetSpawn(Vector3 spawn)
    {
        PlayerPrefs.SetFloat("xSpawn", spawn.x);
        PlayerPrefs.SetFloat("ySpawn", spawn.y);
        PlayerPrefs.SetFloat("zSpawn", spawn.z);
    }
}
