using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    [Tooltip("The reference to the dialogue master, found in the canvas")]
    public GameObject dialogueMaster;
    [Tooltip("The message you want spoken")]
    [TextArea(1, 5)]
    public string message;
    [Header("Conditions")]
    [Tooltip("Whether or not the clip should be allowed to play multiple times")]
    public bool oneTime;
    [Tooltip("Whether or not the player must interact in order to hear the dialogue")]
    public bool mustInteract;
    [Tooltip("The cooldown of the dialogue")]
    public float cooldown;


    //Whether player is in collider
    private bool playerInTrigger;
    //If oneTime is selected, this tracks it
    private bool oneTimeTriggered;

    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        //If dialogue can only be spoken once, this checks whether it already has been
        if (oneTimeTriggered == false)
        {
            //If dialogue can only trigger when interacted with
            if (mustInteract)
            {
                //If player is in range, this triggers it
                if (playerInTrigger && Input.GetButton("Fire1"))
                {
                    Trigger();
                }
            }
            else
            {
                //Otherwise it is automatically triggered
                if (playerInTrigger)
                {
                    Trigger();
                }
            }
        }
    }

    void Trigger()
    {
        //Start the Speak coroutine in the DialogueMaster and set conditions if they were picked
        StartCoroutine(dialogueMaster.GetComponent<ScreenDialogue>().Speak(message, cooldown));
        if (oneTime == true) oneTimeTriggered = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && oneTimeTriggered == false && playerInTrigger == false)
        {
            playerInTrigger = true;
          
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTrigger = false;
        }
    }

}
