using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonMaster : MonoBehaviour {

    public GameObject doorToOpen;
    public List<int> pressOrder = new List<int>();
    public int amountOfButtons;
    private int counter;
    private bool unlocked;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
		if(counter >= amountOfButtons && unlocked == false)
        {
            //doorToOpen.GetComponent<SlidingDoor>().Trigger();
            unlocked = true;
        }
    }

    public void UpdateOrder(int index)
    {
        if (index == pressOrder[counter])
        {
            counter++;
        }
        else counter = 0;
    }
}
