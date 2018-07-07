using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatRandomMovement : MonoBehaviour
{

    [Header("Object Attributes")]
    public GameObject[] movingObjects;
    public int amountOfObjects;

    [Header("Area Attributes")]
    public float areaRadius;

    [Header("Movement Attributes")]
    public float movementSpeed;
    public float rangeMargin;
    [Range(0, 1)]
    public float slerpSpeed;

    private Vector3 originPosition;
    private GameObject[] movingObjectList;
    private Vector3[] previousPositionList;
    private Vector3[] nextPositionList;

    // Use this for initialization
    void Start()
    {
        originPosition = transform.position;
        movingObjectList     = new GameObject[amountOfObjects];
        previousPositionList = new Vector3[amountOfObjects];
        nextPositionList     = new Vector3[amountOfObjects];

        for (int i = 0; i < amountOfObjects; i++)
        {
            Vector3 pV = Random.insideUnitCircle * areaRadius;
            Vector3 nV = Random.insideUnitCircle * areaRadius;
            pV.z = pV.y;
            pV.y = 0;
            nV.z = nV.y;
            nV.y = 0;

            previousPositionList[i] = pV;
            nextPositionList[i]     = nV;
            movingObjectList[i] = GameObject.Instantiate<GameObject>(movingObjects[Random.Range(0, movingObjects.Length - 1)], originPosition + previousPositionList[i], Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < amountOfObjects; i++)
        {
            float distance = Vector3.Distance(previousPositionList[i], nextPositionList[i]);
            float distanceLeft = Vector3.Distance(movingObjectList[i].transform.position - originPosition, nextPositionList[i]);
            Debug.DrawLine(movingObjectList[i].transform.position, nextPositionList[i] + originPosition);

            Quaternion targetRotation = Quaternion.LookRotation(nextPositionList[i] - (movingObjectList[i].transform.position - originPosition));

            movingObjectList[i].transform.rotation = Quaternion.Slerp(movingObjectList[i].transform.rotation, targetRotation, slerpSpeed);
            movingObjectList[i].transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

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
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
