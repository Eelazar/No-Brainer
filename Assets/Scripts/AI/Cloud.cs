using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    
    [Tooltip("Point around which the clouds will rotate")]
    public Vector3 centerOfRotation;
    [Tooltip("Radius of the rotation circle, see editor for a visual representation")]
    public float radius;
    [Tooltip("Every frame, the cloud will rotate by this many degress around its center")]
    public float rotationSpeed;

    [Header("Variants")]
    [Tooltip("If this is enabled the centerOfRotation will be ignored, and the object's position will be used, while the center will be set at 0,0")]
    public bool useObjectPosition;
    [Tooltip("If this is enabled the object will rotate in the opposite direction")]
    public bool reverse;

    void Start()
    {
        if(useObjectPosition == true)
        {
            radius = Mathf.Sqrt((transform.position.x * transform.position.x) + (transform.position.z * transform.position.z));
            centerOfRotation = new Vector3(0, transform.position.y, 0);
        }
    }

    void FixedUpdate ()
    {
        if(reverse == false)
        {
            transform.RotateAround(centerOfRotation, Vector3.up, rotationSpeed);
        }
        else if(reverse == true)
        {
            transform.RotateAround(centerOfRotation, Vector3.up, -rotationSpeed);
        }
    }

    void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.cyan;
        if(reverse == true)
        {
            UnityEditor.Handles.color = Color.red;
        }
        UnityEditor.Handles.DrawWireDisc(centerOfRotation, Vector3.up, radius);
    }
}
