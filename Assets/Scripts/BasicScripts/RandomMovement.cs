using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    [Header("Object Attributes")]
    public GameObject[] movingObjects;
    public int amountOfObjects;

    [Header("Area Attributes")]
    public float areaRadius;

    [Header("Movement Attributes")]
    public float movementSpeed;
    public float rangeMargin;
    public float slerpSpeed;

    private Vector3 originPosition;
    private GameObject[] movingObjectList;
    private Vector3[] previousPositionList;
    private Vector3[] nextPositionList;

    // Use this for initialization
    void Start ()
    {
        originPosition = transform.position;
        movingObjectList     = new GameObject[amountOfObjects];
        previousPositionList = new Vector3[amountOfObjects];
        nextPositionList     = new Vector3[amountOfObjects];

        for (int i = 0; i < amountOfObjects; i++)
        {
            previousPositionList[i] = Random.insideUnitSphere * areaRadius;
            nextPositionList[i]     = Random.insideUnitSphere * areaRadius;
            movingObjectList[i]     = GameObject.Instantiate<GameObject>(movingObjects[Random.Range(0, movingObjects.Length-1)], originPosition + previousPositionList[i], this.transform.rotation);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		for(int i = 0; i < amountOfObjects; i++)
        {
            float distance = Vector3.Distance(previousPositionList[i], nextPositionList[i]);
            float distanceLeft = Vector3.Distance(movingObjectList[i].transform.position - originPosition, nextPositionList[i]);
            Debug.DrawLine(movingObjectList[i].transform.position, nextPositionList[i] + originPosition);

            Quaternion targetRotation = Quaternion.LookRotation(nextPositionList[i] - (movingObjectList[i].transform.position - originPosition));

            //movingObjectList[i].transform.Rotate(transform.up, angle * Time.deltaTime);
            movingObjectList[i].transform.rotation = Quaternion.Slerp(movingObjectList[i].transform.rotation, targetRotation, slerpSpeed);

            movingObjectList[i].transform.Translate(Vector3.forward * Time.deltaTime);
            
            if(distanceLeft < rangeMargin)
            {
                previousPositionList[i] = nextPositionList[i];
                nextPositionList[i] = Random.insideUnitSphere * areaRadius;
            }
        }

        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
