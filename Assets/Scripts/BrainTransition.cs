using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainTransition : MonoBehaviour {

    public float multiplier;
    public string scene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Trigger()
    {
        Initiate.Fade(scene, Color.white, multiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        Trigger();
    }
}
