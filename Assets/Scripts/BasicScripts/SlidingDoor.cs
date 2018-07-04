using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

    public Vector3 slideVector;
    public int slideAmount;
    public float closeSlideDelay, openSlideDelay;

    public bool automatic;

    public bool open;

	// Use this for initialization
	void Start ()
    {
        if (automatic == true)
        {
            Trigger();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }

    public void Trigger()
    {
        if (open)
        {
            StartCoroutine(Close());
            open = false;
        }
        else
        {
            StartCoroutine(Open());
            open = true;
        }
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(closeSlideDelay);
        for (int i = 0; i <= slideAmount; i++)
        {
            gameObject.transform.Translate(-slideVector);
            yield return null;
        }
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(openSlideDelay);
        for (int i = 0; i <= slideAmount; i++)
        {
            gameObject.transform.Translate(slideVector);
            yield return null;
        }
    }
}
