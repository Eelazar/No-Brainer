using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeletePrefs : MonoBehaviour {
    


	void Start () {
        this.GetComponent<Button>().onClick.AddListener(ResetPrefs);
	}
	
	void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu");
    }
}
