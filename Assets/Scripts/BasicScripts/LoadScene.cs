﻿using System.Collections;
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

    public AudioClip loadSound;

    //References
    private GameObject musicObject;
    private Button btn;
    private AudioSource source;

    private bool interacted;


    // Use this for initialization
    void Start ()
    {
        source = GetComponent<AudioSource>();
        musicObject = GameObject.Find("LvlMusic");

        if (sceneToLoad == "Resume")
        {
            if (PlayerPrefs.HasKey("Scene"))
            {
                sceneToLoad = PlayerPrefs.GetString("Scene");
            }
            else
            {
                sceneToLoad = "Tutorial";
            }
            newSpawnPoint = GetSpawn();
        }

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
        source.PlayOneShot(loadSound);
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

    Vector3 GetSpawn()
    {
        Vector3 spawn = new Vector3(PlayerPrefs.GetFloat("xSpawn", 0), PlayerPrefs.GetFloat("ySpawn", 0), PlayerPrefs.GetFloat("zSpawn", 0));
        return spawn;
    }
}
