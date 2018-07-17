using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDialogueTrigger : MonoBehaviour {

    [Tooltip("The text you want to show")]
    public string text;
    [Tooltip("The reference to the PopupMaster, found in the canvas")]
    public GameObject popupMaster;


    private GameObject dialogueMaster;

    void Start()
    {
        dialogueMaster = GameObject.Find("DialogueMaster");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && dialogueMaster.GetComponent<ScreenDialogue>().cd == false)
        {
            DisplayText();                                              
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            RemoveText();
        }
    }

    void DisplayText()
    {
        popupMaster.GetComponent<PopupDialogue>().Write(text);
    }

    void RemoveText()
    {
        popupMaster.GetComponent<PopupDialogue>().Clear();
    }
}
