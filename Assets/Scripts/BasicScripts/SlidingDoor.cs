using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

    [Tooltip("The position of the door when it's opened")]
    public Vector3 closedPosition;
    [Tooltip("The position of the door when it's closed")]
    public Vector3 openPosition;
    [Tooltip("The amount of time it takes for the door to close")]
    public float slideDuration;
    [Tooltip("Whether or not the door is currently open")]
    public bool open;
    [Tooltip("Whether or not the door should instantly activate")]
    public bool automatic;


    //Lerp Variables
    private float lerpT;
    private float lerpStart;

	
	void Start ()
    {
        if (open)
        {
            transform.position = openPosition;
        }
        else
        {
            transform.position = closedPosition;
        }

        if (automatic == true)
        {
            StartCoroutine(Trigger());
        }
    }
	
    public IEnumerator Trigger()
    {
        lerpStart = Time.time;

        if (open)
        {
            while (lerpT < 1)
            {
                lerpT = (Time.time - lerpStart) / slideDuration;
                transform.position = Vector3.Lerp(openPosition, closedPosition, lerpT);
                yield return null;
            }

            open = false;
        }
        else if(!open)
        {
            while (lerpT < 1)
            {
                lerpT = (Time.time - lerpStart) / slideDuration;
                transform.position = Vector3.Lerp(closedPosition, openPosition, lerpT);
                yield return null;
            }

            open = true;
        }
    }
}
