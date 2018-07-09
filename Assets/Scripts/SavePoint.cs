using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    [Tooltip("The new spawn position that will be saved to PlayerPrefs")]
    public Vector3 spawnPosition;
    [Tooltip("The new scene that will be saved to PlayerPrefs")]
    public string scene;
    [Tooltip("The new preview scene that will be saved to PlayerPrefs")]
    public string previewScene;

    //Resets when the player leaves the collider
    private bool saved;
    

    void OnTriggerEnter(Collider collider)
    {
        //If the player enters the collider, save all values to PlayerPrefs
        if(collider.tag == "Player" && saved == false)
        {
            PlayerPrefs.SetFloat("xSpawn", spawnPosition.x);
            PlayerPrefs.SetFloat("ySpawn", spawnPosition.y);
            PlayerPrefs.SetFloat("zSpawn", spawnPosition.z);
            PlayerPrefs.SetString("Scene", scene);
            PlayerPrefs.SetString("ScenePreview", previewScene);

            //Save all changes in case of a crash
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

    private void OnGUI()
    {
        if(saved == true)
        {
            GUI.TextField(new Rect(0, 0, 50, 25), "Saved!");
        }
    }
}
