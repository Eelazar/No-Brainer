using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchChange : MonoBehaviour {

    public float newPitch;
    public AudioSource music;

    private bool changed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && changed == false)
        {
            changed = true;
            music.pitch = newPitch;
        }
    }
}
