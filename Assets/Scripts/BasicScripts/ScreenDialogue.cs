using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDialogue : MonoBehaviour {

    public Text dialogueBox;
    public float delay;
    public float commaDelay;
    public float dotDelay;
    public float displayTime;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Speak("Blablablabla, blablablablablablablab... lablablablablabla. blabla"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Speak(string message)
    {
        for(int i = 0; i < message.Length; i++)
        {
            dialogueBox.text += message[i];
            if(message[i].ToString() == ".")
            {
                yield return new WaitForSeconds(dotDelay);
            }
            else if (message[i].ToString() == ",")
            {
                yield return new WaitForSeconds(commaDelay);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }

        yield return new WaitForSeconds(displayTime);
        dialogueBox.text = "";
    }
}
