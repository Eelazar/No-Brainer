using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorTrigger : MonoBehaviour {

    public GameObject door;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            door.GetComponent<SlidingDoor>().StartCoroutine(door.GetComponent<SlidingDoor>().Trigger());
        }
    }
}
