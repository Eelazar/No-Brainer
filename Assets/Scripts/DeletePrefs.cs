using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePrefs : MonoBehaviour {
    


	void Start () {
        this.GetComponent<Button>().onClick.AddListener(ResetPrefs);
	}
	
	void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
