using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour {
    
    public float stayAngle, decelerateAngle, numberOfTurns, maxAcceleration;

    private bool triggerTurn;
    private float rotationIndex, acceleration;
    

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (triggerTurn)
        {
            Spin();
        }
	}

    void Spin()
    {
        float decRange = (360 * numberOfTurns) - decelerateAngle;

        if (rotationIndex <= 360 * numberOfTurns)
        {
            if(rotationIndex < stayAngle)
            {
                rotationIndex += acceleration * Time.deltaTime;
                gameObject.transform.RotateAround(Vector3.zero, Vector3.up, acceleration * Time.deltaTime);
                
                acceleration = Mathf.Lerp(1, maxAcceleration, (rotationIndex) / stayAngle);
            }
            else if (rotationIndex < decelerateAngle)
            {
                rotationIndex += acceleration * Time.deltaTime;
                gameObject.transform.RotateAround(Vector3.zero, Vector3.up, acceleration * Time.deltaTime);                
            }
            else if(rotationIndex < 360 * numberOfTurns)
            {
                rotationIndex += acceleration * Time.deltaTime;
                gameObject.transform.RotateAround(Vector3.zero, Vector3.up, acceleration * Time.deltaTime);
                
                acceleration = Mathf.Lerp(maxAcceleration, 0, (rotationIndex - decelerateAngle) / decRange);
            }
            else
            {
                triggerTurn = false;
            }
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 30), "Spin"))
        {
            triggerTurn = true;
            rotationIndex = 0;
            acceleration = 0;
        }
    }
}
