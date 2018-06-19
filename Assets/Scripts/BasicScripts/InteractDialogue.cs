using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDialogue : MonoBehaviour {

    [TextArea(1, 5)]
    public string dialogue;

    public float xPosition, yPosition, width, height, displayTime;

    private bool triggered;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if(triggered == true)
        {
            GUI.TextArea(new Rect(xPosition, yPosition, width, height), dialogue);
        }
    }

    public IEnumerator Trigger()
    {
        triggered = true;
        yield return new WaitForSeconds(displayTime);
        triggered = false;
    }

    public void Close()
    {
        triggered = false;
    }
}
