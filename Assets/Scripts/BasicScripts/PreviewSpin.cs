using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSpin : MonoBehaviour
{

    public float spinSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.RotateAround(Vector3.zero, Vector3.up, spinSpeed * Time.deltaTime);
    }
}
