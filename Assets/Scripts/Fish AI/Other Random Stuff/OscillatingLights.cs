using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingLights : MonoBehaviour
{

    [Header("Object Attributes")]
    public GameObject[] movingObjects;
    public int amountOfObjects;

    [Header("Area Attributes")]
    public float areaRadius;

    [Header("Movement Attributes")]
    public float movementSpeed;
    public float positionMargin;
    [Range(0, 1)]
    public float slerpSpeed;

    [Header("Light Attributes")]
    public bool randomIntensity;
    public float minIntensity;
    public float maxIntensity;
    [Range(0, 1)]
    public float intensitySpeed;
    public float intensityMargin;
    [Space]
    public bool randomRange;
    public float minRange;
    public float maxRange;
    [Range(0, 1)]
    public float rangeSpeed;
    public float rangeMargin;

    private Vector3 originPosition;
    private GameObject[] movingObjectList;
    private Vector3[] previousPositionList;
    private Vector3[] nextPositionList;
    private float[] previousIntensityList;
    private float[] nextIntensityList;
    private float[] previousRangeList;
    private float[] nextRangeList;

    // Use this for initialization
    void Start()
    {
        originPosition = transform.position;
        movingObjectList      = new GameObject[amountOfObjects];
        previousPositionList  = new Vector3[amountOfObjects];
        nextPositionList      = new Vector3[amountOfObjects];
        previousIntensityList = new float[amountOfObjects];
        nextIntensityList     = new float[amountOfObjects];
        previousRangeList     = new float[amountOfObjects];
        nextRangeList         = new float[amountOfObjects];

        for (int i = 0; i < amountOfObjects; i++)
        {
            previousPositionList[i]  = Random.insideUnitSphere * areaRadius;
            nextPositionList[i]      = Random.insideUnitSphere * areaRadius;
            movingObjectList[i]      = GameObject.Instantiate<GameObject>(movingObjects[Random.Range(0, movingObjects.Length - 1)], originPosition + previousPositionList[i], this.transform.rotation);

            if (randomIntensity)
            {
                previousIntensityList[i] = Random.Range(minIntensity, maxIntensity);
                nextIntensityList[i] = Random.Range(minIntensity, maxIntensity);
                movingObjectList[i].GetComponent<Light>().intensity = previousIntensityList[i];
            }

            if (randomRange)
            {
                previousRangeList[i] = Random.Range(minRange, maxRange);
                nextRangeList[i] = Random.Range(minRange, maxRange);
                movingObjectList[i].GetComponent<Light>().range = previousRangeList[i];
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < amountOfObjects; i++)
        {
            //Random Movement
            float distance = Vector3.Distance(previousPositionList[i], nextPositionList[i]);
            float distanceLeft = Vector3.Distance(movingObjectList[i].transform.position - originPosition, nextPositionList[i]);
            Debug.DrawLine(movingObjectList[i].transform.position, nextPositionList[i] + originPosition);

            Quaternion targetRotation = Quaternion.LookRotation(nextPositionList[i] - (movingObjectList[i].transform.position - originPosition));

            movingObjectList[i].transform.rotation = Quaternion.Slerp(movingObjectList[i].transform.rotation, targetRotation, slerpSpeed);

            movingObjectList[i].transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            if (distanceLeft < positionMargin)
            {
                previousPositionList[i] = nextPositionList[i];
                nextPositionList[i] = Random.insideUnitSphere * areaRadius;
            }

            //Random Intensity and Range
            movingObjectList[i].GetComponent<Light>().intensity = Mathf.Lerp(movingObjectList[i].GetComponent<Light>().intensity, nextIntensityList[i], intensitySpeed);
            movingObjectList[i].GetComponent<Light>().range     = Mathf.Lerp(movingObjectList[i].GetComponent<Light>().range, nextRangeList[i], rangeSpeed);

            if(nextIntensityList[i] - movingObjectList[i].GetComponent<Light>().intensity < intensityMargin)
            {
                previousIntensityList[i] = nextIntensityList[i];
                nextIntensityList[i] = Random.Range(minIntensity, maxIntensity);
            }
            if (nextRangeList[i] - movingObjectList[i].GetComponent<Light>().range < rangeMargin)
            {
                previousRangeList[i] = Random.Range(minRange, maxRange);
                nextRangeList[i] = Random.Range(minRange, maxRange);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
