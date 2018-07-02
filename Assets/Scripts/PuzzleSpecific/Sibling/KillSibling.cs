using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSibling : MonoBehaviour {

    public GameObject sibling;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deadly")
        {
            sibling.GetComponent<Sibling>().Respawn();
        }
    }

}
