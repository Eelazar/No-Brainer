using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVerticalFollow : MonoBehaviour {

    public GameObject target;
    public float followSpeed;

    private float currentHeight;
    private Vector3 velocity, basePosition;

	// Use this for initialization
	void Start ()
    {
        basePosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentHeight = target.transform.position.y;

        velocity.y = Mathf.Lerp(0, currentHeight, followSpeed);
        gameObject.transform.position = velocity + basePosition;
    }
}
