using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVerticalFollow : MonoBehaviour {

    public GameObject target;
    public float followSpeed;

    private float currentHeight;
    private Vector3 newPosition, basePosition, velocity;

	// Use this for initialization
	void Start ()
    {
        basePosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentHeight = target.transform.position.y;
        newPosition.y = currentHeight;

        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, newPosition + basePosition, ref velocity, followSpeed);
    }
}
