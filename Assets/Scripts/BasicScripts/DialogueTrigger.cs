using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public GameObject audioMaster;
    public string message;
    public bool oneTime;
    public bool mustInteract;
    public float cooldown;

    private bool activated;
    private bool triggered;
    private bool cd;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(cd == false)
        {
            if (mustInteract == true)
            {
                if (triggered == true)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        Trigger();
                    }
                }
            }
            else if (mustInteract == false)
            {
                if (triggered == true)
                {
                    Trigger();
                }
            }
        }
    }

    void Trigger()
    {
        if (activated == false)
        {
            StartCoroutine(audioMaster.GetComponent<ScreenDialogue>().Speak(message));
            activated = true;
            cd = true;
            StartCoroutine(Cooldown());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggered = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(oneTime == false)
            {
                activated = false;
            }
        }

    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        cd = false;
    }
}
