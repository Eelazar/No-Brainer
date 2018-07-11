using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayerStay : MonoBehaviour {

    public float range;

    private float distance;
    private GameObject player;
    private Vector3 origin;

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
            Vector3 v = this.transform.position - player.transform.position;
            v.Normalize();
            v *= (range - distance);
            v.y = 0;
            this.transform.position += v;
        }
    }
}
