using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RndMovement : MonoBehaviour {

    public float radius, speed, rotationSpeed, reachRange, lerpT;
    
    private Vector3 destination, distance;
    private float nextIntensity;
    

    // Use this for initialization
    void Start () {
        FindNextTarget();
        nextIntensity = this.GetComponent<Light>().intensity;

    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.Translate(Vector3.forward * speed);        

        if(Vector3.Distance(transform.position, destination) < reachRange)
        {
            FindNextTarget();
        }

        if(this.GetComponent<Light>().intensity < nextIntensity + 0.1 && this.GetComponent<Light>().intensity > nextIntensity - 0.1)
        {
            nextIntensity = Random.Range(0.4F, 1F);
        }
        this.GetComponent<Light>().intensity = Mathf.Lerp(this.GetComponent<Light>().intensity, nextIntensity, lerpT);
	}

    void FindNextTarget()
    {
        destination = transform.parent.transform.position;
        destination += (Random.insideUnitSphere * radius);
    }    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.parent.transform.position, radius);
    }
    
}
