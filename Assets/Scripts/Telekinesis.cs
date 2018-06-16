using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour {

    public float distanceHeld, heightHeld;
    public Vector3 pushForce, rotationVector;
    public bool rotation;

    private GameObject objHeld;
    private GameObject objInRange;
    private Vector3 objPosition;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		if(objHeld != null)
        {
            objPosition = gameObject.transform.position;
            objPosition.z += distanceHeld;
            objHeld.transform.position = objPosition;
            if (rotation)
            {
                objHeld.transform.Rotate(rotationVector);
            }
        }

        if (Input.GetButtonDown("Fire1") && objHeld != null)
        {
            objHeld = null;
        }
        else if (Input.GetButtonDown("Fire2") && objHeld != null)
        {
            objHeld.GetComponent<Rigidbody>().AddForce(pushForce, ForceMode.Impulse);
            objHeld = null;
        }
        else if (Input.GetButtonDown("Fire1") && objInRange != null)
        {
            objHeld = objInRange;
        }
	}

    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "TKobj")
        {
            objInRange = obj.gameObject;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.tag == "TKobj")
        {
            objInRange = null;
        }
    }
}
