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

    [Header("References")]
    public GameObject display;

    //Resets when the player leaves the collider
    private bool saved;
    private bool showDisplay;
    


    void Update()
    {
        if (showDisplay)
        {
            display.SetActive(true);
        }
        else display.SetActive(false);
    }

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
            showDisplay = true;
            StartCoroutine(DisplayTime());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player" && saved == true)
        {
            saved = false;
        }
    }    

    IEnumerator DisplayTime()
    {
        yield return new WaitForSeconds(3);
        showDisplay = false;
    }
}
