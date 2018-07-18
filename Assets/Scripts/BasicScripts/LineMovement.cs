using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMovement : MonoBehaviour {

    public Vector3 pointA, pointB;
    public float speed, waitTime, distanceThreshold;

    //Target is B if true, A if false
    private bool platformSwitch, wait;

    private Vector3 directionToB, directionToA, newPosition;

	// Use this for initialization
	void Start ()
    {
        platformSwitch = true;
        gameObject.transform.position = pointA;
        directionToA = (pointA - pointB).normalized;
        directionToB = (pointB - pointA).normalized;
    }

    // Update is called once per frame
    void Update ()
    {
        if (platformSwitch == true && wait == false)
        {
            transform.rotation = Quaternion.LookRotation(directionToB);
            if (Vector3.Distance(gameObject.transform.position, pointB) < distanceThreshold)
            {
                wait = true;
                StartCoroutine(Wait());
            }
            newPosition = gameObject.transform.position + (directionToB * speed);
            gameObject.GetComponent<Rigidbody>().MovePosition(newPosition);
        }
        else if (platformSwitch == false && wait == false)
        {
            transform.rotation = Quaternion.LookRotation(directionToA);
            if (Vector3.Distance(gameObject.transform.position, pointA) < distanceThreshold)
            {
                wait = true;
                StartCoroutine(Wait());
            }
            newPosition = gameObject.transform.position + (directionToA * speed);
            gameObject.GetComponent<Rigidbody>().MovePosition(newPosition);
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        platformSwitch = !platformSwitch;
        wait = false;
    }
    
}
