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
    [Tooltip("Delay that will be applied every time the door is triggered")]
    public float delay;
    [Tooltip("The animation curve for the door's movement")]
    public AnimationCurve animCurve;
    [Tooltip("The sound the door makes when starting to move")]
    public AudioClip startSound;
    [Tooltip("The sound the door makes when stopping")]
    public AudioClip stopSound;



    //References
    private AudioSource source;
    //Lerp Variables
    private float lerpT;
    private float lerpStart;

	
	void Start ()
    {
        source = GetComponent<AudioSource>();

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
        yield return new WaitForSeconds(delay);

        lerpStart = Time.time;

        if(startSound != null) source.PlayOneShot(startSound);

        if (open)
        {
            while (lerpT < 1)
            {
                lerpT = (Time.time - lerpStart) / slideDuration;
                transform.position = Vector3.LerpUnclamped(openPosition, closedPosition, animCurve.Evaluate(lerpT));
                yield return null;
            }

            open = false;
        }
        else if(!open)
        {
            while (lerpT < 1)
            {
                lerpT = (Time.time - lerpStart) / slideDuration;
                transform.position = Vector3.Lerp(closedPosition, openPosition, animCurve.Evaluate(lerpT));
                yield return null;
            }

            open = true;
        }

        if (stopSound != null) source.PlayOneShot(stopSound);
    }
}
