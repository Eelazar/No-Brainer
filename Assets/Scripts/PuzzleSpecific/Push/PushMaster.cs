using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMaster : MonoBehaviour {

    public GameObject doorToOpen;
    public int amountOfPieces;

    public bool[] puzzlePieces;

    private bool completed;



    // Use this for initialization
    void Start()
    {
        puzzlePieces = new bool[amountOfPieces];
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(completed == false)
        {
            completed = true;
            foreach(bool b in puzzlePieces)
            {
                if (b == false) completed = false;
            }
        }
        else
        {
            doorToOpen.GetComponent<SlidingDoor>().Trigger();
        }
	}
    
}
