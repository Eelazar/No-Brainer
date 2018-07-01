using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sibling : MonoBehaviour {

    public Vector3 spawn;
    public GameObject brother;

    private Vector3 distance;

	// Use this for initialization
	void Start () {
        distance.x = gameObject.transform.position.x - brother.transform.position.x;
        distance.z = gameObject.transform.position.z - brother.transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 v = new Vector3(brother.transform.position.x + distance.x, gameObject.transform.position.y, brother.transform.position.z + distance.z);
        gameObject.transform.position = v;
	}

    void OnTriggerEnter(Collider other)
    {
        //Respawn the player if he collides with a deadly object
        if (other.tag == "Deadly")
        {
            gameObject.transform.position = spawn;
        }
    }
}
