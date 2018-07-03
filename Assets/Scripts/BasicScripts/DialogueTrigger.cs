using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public GameObject audioMaster;
    [TextArea(1, 5)]
    public string message;
    public bool oneTime;
    public bool mustInteract;
    public float cooldown;

    private bool playerInTrigger;
    private bool oneTimeTriggered;
    private bool cd;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cd == false && oneTimeTriggered == false)
        {
            if (mustInteract)
            {
                if (playerInTrigger && Input.GetButton("Fire1"))
                {
                    Trigger();
                }
            }
            else
            {
                if (playerInTrigger)
                {
                    Trigger();
                }
            }
        }
    }

    void Trigger()
    {
        StartCoroutine(audioMaster.GetComponent<ScreenDialogue>().Speak(message));
        StartCoroutine(Cooldown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && oneTimeTriggered == false)
        {
            playerInTrigger = true;
            if (oneTime == true) oneTimeTriggered = true;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTrigger = false;
        }
    }

    IEnumerator Cooldown()
    {
        cd = true;
        yield return new WaitForSeconds(cooldown);
        cd = false;
    }
}
