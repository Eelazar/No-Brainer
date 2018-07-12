using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour {
    
    [Header("Movement Attributes")]
    [Tooltip("Speed objects will move at")]
    public float movementSpeed;
    [Tooltip("Distance at which objects will consider their destination to be reached")]
    public float rangeMargin;
    [Tooltip("Amount of time it will take until the object is rotated")]
    public float rangeChangeDuration;
    [Tooltip("Maximum amount of time the fish will wait upon reaching its destination. Min is 0")]
    public float maxWaitTime;


    [HideInInspector]
    public Bounds moveArea;

    //Variables
    private bool waiting;
    private Vector3 destination;
    private Quaternion targetRotation;

    //Time variables for lerping
    private float rotationLerp;
    private float rotationStartTime;


    void Start()
    {
        destination = FindNewDestination();
    }


    void Update()
    {
        if (!waiting)
        {
            //Calculate distance between object and target
            float distanceLeft = Vector3.Distance(this.transform.position, destination);
            
            //Calculate rotation needed to face target
            targetRotation = Quaternion.LookRotation(destination - this.transform.position);

            //Calculate the lerp t variable based on the duration of the rotation
            rotationLerp = (Time.time - rotationStartTime) / rangeChangeDuration;
            //Slerp that shit
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationLerp);
            //Rotate that shit
            this.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            //If the object has reached its target, calculate a new destination and reset the lerp variable
            if (distanceLeft < rangeMargin)
            {
                destination = FindNewDestination();
                rotationStartTime = Time.time;

                //Set the movement cooldown, of course if CD is 0 the obect will continue instantly
                StartCoroutine(Wait());
            }
        }
    }
    

    IEnumerator Wait()
    {
        waiting = true;
        float t = Random.Range(0, maxWaitTime);
        yield return new WaitForSeconds(t);
        waiting = false;
    }

    Vector3 FindNewDestination()
    {
        Vector3 position;
        position.x = Random.Range(moveArea.min.x, moveArea.max.x);
        position.y = transform.position.y;
        position.z = Random.Range(moveArea.min.z, moveArea.max.z);

        return position;
    }

    private void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(destination, 0.5F);
#endif

    }
}
