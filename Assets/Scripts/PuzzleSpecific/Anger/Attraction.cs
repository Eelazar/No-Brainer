using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Attraction : MonoBehaviour {

    public float attractionRange;
    public float attractionForce;
    public bool stayAttracted;

    private Collider[] attractableObjects;
    private GameObject player;
    private float distance;
    private Vector3 direction;
    private bool attracted = false;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (attracted == false)
        {
            SearchAttractableObjects();
        }
        else if(attracted == true)
        {
            distance = Vector3.Distance(this.transform.position, player.transform.position);
            direction = player.transform.position - this.transform.position;
            direction.Normalize();

            if (stayAttracted == true)
            {
                transform.GetComponent<Rigidbody>().AddForce(direction * attractionForce);
            }
            else if(distance <= attractionRange)
            {
                transform.GetComponent<Rigidbody>().AddForce(direction * attractionForce);
            }
            else if(distance > attractionRange)
            {

            }
        }
       
	}

    void SearchAttractableObjects()
    {
        attractableObjects = Physics.OverlapSphere(transform.position, attractionRange);
        foreach (Collider c in attractableObjects)
        {
            if (c.tag == "Player")
            {
                player = c.gameObject;
                attracted = true;
            }            
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(gameObject.transform.position, Vector3.up, attractionRange);
    }
#endif
}
