using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorTrigger : MonoBehaviour {

    public GameObject door;
    public float minTimePassed;
	
	private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && Time.time > minTimePassed && triggered == false)
			{
				door.GetComponent<SlidingDoor>().StartCoroutine(door.GetComponent<SlidingDoor>().Trigger());
				triggered = true;
			}
    }
}
