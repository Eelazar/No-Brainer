using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string sceneToLoad;
    public Vector3 newSpawnPoint;
    public bool mustInteract;

    public bool destroyMusic;
    public bool loadAfterTime;
    public float timeToLoad;


    private GameObject musicObject;
    private Button btn;

    private bool interacted;


    // Use this for initialization
    void Start ()
    {
        musicObject = GameObject.Find("LvlMusic");

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
        if(loadAfterTime == true && timeToLoad < Time.time)
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

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            interacted = false;
        }
    }

    void LoadNewScene()
    {
        if (destroyMusic)
        {
            Destroy(musicObject);
        }
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
