using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour {

    public Color pushColor;
    public float pushDepth, pushDelay, pushStep;
    public int buttonIndex;

    public bool pressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(PushIn());
            pressed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PushOut());
            pressed = false;
        }
    }

    IEnumerator PushIn()
    {
        yield return new WaitForSeconds(pushDelay);
        for (int i = 0; i <= pushDepth; i++)
        {
            gameObject.transform.Translate(new Vector3(0, -pushStep, 0));
            yield return null;
        }
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", pushColor);
        //GetComponentInParent<PushButtonMaster>().UpdateOrder(buttonIndex);
    }

    IEnumerator PushOut()
    {
        yield return new WaitForSeconds(pushDelay);
        for (int i = 0; i <= pushDepth; i++)
        {
            gameObject.transform.Translate(new Vector3(0, pushStep, 0));
            yield return null;
        }
        Color hdrColor = Color.black;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", hdrColor);
    }
}
