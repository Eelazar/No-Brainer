using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSibling : MonoBehaviour {

    public float delay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sibling")
        {
            Destroy(other.gameObject, delay);
        }
    }
}
