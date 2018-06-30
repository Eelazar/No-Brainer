using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    public Vector3 spawnPosition;
    public string scene;
    public string previewScene;

    private bool saved;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player" && saved == false)
        {
            PlayerPrefs.SetFloat("xSpawn", spawnPosition.x);
            PlayerPrefs.SetFloat("ySpawn", spawnPosition.y);
            PlayerPrefs.SetFloat("zSpawn", spawnPosition.z);
            PlayerPrefs.SetString("Scene", scene);
            PlayerPrefs.SetString("PreviewScene", previewScene);
            PlayerPrefs.Save();
            saved = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player" && saved == true)
        {
            saved = false;
        }
    }
}
