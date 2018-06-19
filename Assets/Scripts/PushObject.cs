using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour {
    
    private GameObject pushObject;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(pushObject != null && Input.GetButton("Fire1"))
        {
            pushObject.GetComponent<PushableObject>().Push(transform);
        }        
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PushableObject>() != null)
        {
            pushObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PushableObject>() != null)
        {
            pushObject = null;
        }
    }
}
