﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    [Header("Movement Attributes")]
    [Tooltip("Speed at which the fish will move")]
    public float movementSpeed;
    [Range(0, 0.05F)]
    [Tooltip("Speed at which the fish will rotate")]
    public float slerpSpeed;
    [Tooltip("Limit the vertical distance the fish may travel (infinite if 0)")]
    public float staminaVertical;
    [Tooltip("Maximum amount of time the fish will wait upon reaching its destination. Min is 0")]
    public float maxWaitTime;

    [Header("Extra Randomization")]
    [Tooltip("Amount of degrees by which the fishes rotation may change while it is swimming (total range = 2 x this, zRotation = range/2)")]
    public float maxRotationVariation;
    [Tooltip("Amount of time the fish will stay on course before picking a new random rotation (final duration = this +- 0->1s")]
    public float rotationVariationDuration;
    [Tooltip("If the distance to the destination is smaller than this value, the fish will stop randomizing and stay on course")]
    public float minDistanceForVariation;

    [Header("Miscellaneous")]
    [Tooltip("Distance from the target position at which the fish will assume he reached his position")]
    public float rangeMargin;


    [HideInInspector]
    public Bounds swimArea;


    //The next destination the fish will aim for
    private Vector3 destination;
    //Whether or not the fish is currently waiting;
    private bool waiting;
    //Whether or not the fish can variate its rotation again
    private bool rotateOnCD;
    //The rotation to the goal
    Quaternion targetRotation;


    void Start ()
    {
        destination = FindNewDestination();
	}


    void Update()
    {
        if (!waiting)
        {
            float distanceLeft = Vector3.Distance(this.transform.position, destination);

            if(rotateOnCD == false)
            {
                targetRotation = Quaternion.LookRotation(destination - this.transform.position);
            }
            if(distanceLeft > minDistanceForVariation && rotateOnCD == false)
            {
                targetRotation = RandomizeRotation(targetRotation);
                StartCoroutine(RotationVariationCD());
            }

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, slerpSpeed);

            this.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            if (distanceLeft < rangeMargin)
            {
                destination = FindNewDestination();
                StartCoroutine(Wait());
            }
        }
    }

    Vector3 FindNewDestination()
    {
        Vector3 position;
        position.x = Random.Range(swimArea.min.x, swimArea.max.x);
        position.y = Random.Range(swimArea.min.y, swimArea.max.y);
        position.z = Random.Range(swimArea.min.z, swimArea.max.z);

        //Limit the vertical distance traveled
        if(staminaVertical != 0)
        {
            while (Mathf.Abs(position.y - this.transform.position.y) > staminaVertical)
            {
                if (position.y - this.transform.position.y > 0) position.y -= 1;
                else if (position.y - this.transform.position.y < 0) position.y += 1;
            }
        }


        return position;
    }

    IEnumerator Wait()
    {
        waiting = true;
        float t = Random.Range(0, maxWaitTime);
        yield return new WaitForSeconds(t);
        waiting = false;
    }

    Quaternion RandomizeRotation(Quaternion rotation)
    {
        float xChange = Random.Range(-maxRotationVariation, maxRotationVariation);
        float yChange = Random.Range(-maxRotationVariation, maxRotationVariation);
        float zChange = Random.Range(-maxRotationVariation /2, maxRotationVariation /2);

        rotation.eulerAngles += new Vector3(xChange, yChange, zChange);
        return rotation;
    }

    IEnumerator RotationVariationCD()
    {
        rotateOnCD = true;
        yield return new WaitForSeconds(rotationVariationDuration + Random.Range(-1, 1));
        rotateOnCD = false;
    }



    private void OnDrawGizmos()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(destination, 0.5F);
#endif

    }
}