using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatRandomMovement : MonoBehaviour
{
    [Header("Object Attributes")]
    [Tooltip("Objects you want to spawn, will be chosen randomly upon creation")]
    public GameObject[] movingObjects;
    [Tooltip("Amount of objects you want to spawn")]
    public int amountOfObjects;

    [Header("Area Attributes")]
    [Tooltip("Radius within which objects will spawn and move")]
    public float areaRadius;

    [Header("Movement Attributes")]
    [Tooltip("Speed objects will move at")]
    public float movementSpeed;
    [Tooltip("Distance at which objects will consider their destination to be reached")]
    public float rangeMargin;
    [Tooltip("Speed objects will rotate at")]
    [Range(0, 1)]
    public float slerpSpeed;


    //Variables
    private Vector3 originPosition;
    private GameObject[] movingObjectList;
    private Vector3[] previousPositionList;
    private Vector3[] nextPositionList;
    

    void Start()
    {
        //Initialize variables
        originPosition = transform.position;
        movingObjectList     = new GameObject[amountOfObjects];
        previousPositionList = new Vector3[amountOfObjects];
        nextPositionList     = new Vector3[amountOfObjects];

        //Create the first set of random values
        for (int i = 0; i < amountOfObjects; i++)
        {
            Vector3 pV = Random.insideUnitCircle * areaRadius;
            Vector3 nV = Random.insideUnitCircle * areaRadius;

            //Since insideUnitCircle circles point forwards rather than up, assign y values to z and set y to 0
            pV.z = pV.y;
            pV.y = 0;
            nV.z = nV.y;
            nV.y = 0;

            //Assign values to objects
            previousPositionList[i] = pV;
            nextPositionList[i]     = nV;
            //Create Objects
            movingObjectList[i] = GameObject.Instantiate<GameObject>(movingObjects[Random.Range(0, movingObjects.Length - 1)], originPosition + previousPositionList[i], Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }
    
    void FixedUpdate()
    {
        for (int i = 0; i < amountOfObjects; i++)
        {
            //Get the distance from the object to its destination
            float distanceLeft = Vector3.Distance(movingObjectList[i].transform.position - originPosition, nextPositionList[i]);
            //Optional visual representation of the path
            //Debug.DrawLine(movingObjectList[i].transform.position, nextPositionList[i] + originPosition);

            //Get the rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(nextPositionList[i] - (movingObjectList[i].transform.position - originPosition));

            //Rotate the object towards the target with Slerp
            movingObjectList[i].transform.rotation = Quaternion.Slerp(movingObjectList[i].transform.rotation, targetRotation, slerpSpeed);
            //Move the objet forward by speed
            movingObjectList[i].transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            //If object is within range of the target, create a new set of random values
            if (distanceLeft < rangeMargin)
            {
                Vector3 nV = Random.insideUnitCircle * areaRadius;
                nV.z = nV.y;
                nV.y = 0;

                previousPositionList[i] = nextPositionList[i];
                nextPositionList[i] = nV;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Visual aid, shows the radius in the editor
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
