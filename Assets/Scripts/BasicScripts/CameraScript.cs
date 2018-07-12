using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float smoothTime;
    public float cameraHeight;
    public bool lockHorizontalMovement;

    private GameObject target;
    private Vector3 newPosition;
    private Vector3 originPosition;

    //Ref Value
    private Vector3 velocity;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        originPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (lockHorizontalMovement)
        {
            newPosition = new Vector3(cameraHeight / 3 * 2, target.transform.position.y + cameraHeight, -cameraHeight / 3 * 2);
        }
        else
        {
            newPosition = new Vector3(target.transform.position.x + cameraHeight / 3 * 2, target.transform.position.y + cameraHeight, target.transform.position.z - cameraHeight / 3 * 2);
        }

        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, newPosition, ref velocity, smoothTime);
	}
}
