using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayerStay : MonoBehaviour {

    public float range;
    public float pushForce;

    private float distance;
    private GameObject player;
    private Vector3 origin;
    private Vector3 pushVector;


    // Use this for initialization
    void Start ()
    {
        origin = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if(distance < range)
        {
            Vector3 v = player.transform.position - gameObject.transform.position;
            v.Normalize();
            v *= pushForce;
            v.y = 0;

            Debug.Log(v);

            gameObject.GetComponent<Rigidbody>().AddForce(-v);
        }
    }
}
