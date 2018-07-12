using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Attraction : MonoBehaviour {

    public float attractionRange;
    public float attractionForce;
    public bool stayAttracted;

    private Collider[] attractableObjects;
    private float distance;
    private bool attracted;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Profiler.BeginSample("AttractScript");
        SearchAttractableObjects();
        foreach (Collider c in attractableObjects)
        {
            distance = Vector3.Distance(this.transform.position, c.transform.position);

            if (attracted)
            {
                Vector3 direction = transform.position - c.transform.position;
                if (c.GetComponent<Rigidbody>() != null)
                {
                    c.GetComponent<Rigidbody>().AddForce(direction * attractionForce);
                }
            }
            else if (distance <= attractionRange && attracted == false)
            {
                Vector3 direction = transform.position - c.transform.position;
                if(c.GetComponent<Rigidbody>() != null)
                {
                    c.GetComponent<Rigidbody>().AddForce(direction * attractionForce);
                }
                attracted = true;
            }
        }
        Profiler.EndSample();
	}

    void SearchAttractableObjects()
    {
        attractableObjects = Physics.OverlapSphere(transform.position, attractionRange);
    }
}
