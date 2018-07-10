using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingLights : MonoBehaviour
{
    [Header("Object Attributes")]
    [Tooltip("Lights you want to spawn, will be chosen randomly upon creation")]
    public GameObject[] movingObjects;
    [Tooltip("Amount of lights you want to spawn")]
    public int amountOfObjects;

    [Header("Area Attributes")]
    [Tooltip("Radius within which Lights will spawn and move")]
    public float areaRadius;

    [Header("Movement Attributes")]
    [Tooltip("Speed lights will move at")]
    public float movementSpeed;
    [Tooltip("Distance at which lights will consider their destination to be reached")]
    public float positionMargin;
    [Tooltip("Speed lights will rotate at")]
    [Range(0, 0.1F)]
    public float slerpSpeed;

    [Header("Light Attributes")]
    [Tooltip("Minimum Intensity")]
    public float minIntensity;
    [Tooltip("Maximum Intensity")]
    public float maxIntensity;
    [Tooltip("Amount of time it will take until the target intensity is achieved")]
    public float intensityChangeDuration;
    [Space]
    [Tooltip("Minimum Range")]
    public float minRange;
    [Tooltip("Maximum Range")]
    public float maxRange;
    [Tooltip("Amount of time it will take until the target range is achieved")]
    public float rangeChangeDuration;


    //Original position of the spawner
    private Vector3 originPosition;
    //Array of all lights as well as their positions, intensity, and range
    private GameObject[] movingObjectList;
    private Vector3[] previousPositionList;
    private Vector3[] nextPositionList;
    private float[] previousIntensityList;
    private float[] nextIntensityList;
    private float[] previousRangeList;
    private float[] nextRangeList;
    //Time variables for lerping
    private float[] intensityLerp;
    private float[] rangeLerp;
    private float[] intensityStartTime;
    private float[] rangeStartTime;


    void Start()
    {
        //Initialization
        originPosition = transform.position;
        movingObjectList      = new GameObject[amountOfObjects];
        previousPositionList  = new Vector3[amountOfObjects];
        nextPositionList      = new Vector3[amountOfObjects];
        previousIntensityList = new float[amountOfObjects];
        nextIntensityList     = new float[amountOfObjects];
        previousRangeList     = new float[amountOfObjects];
        nextRangeList         = new float[amountOfObjects];
        intensityLerp         = new float[amountOfObjects];
        rangeLerp             = new float[amountOfObjects];
        intensityStartTime    = new float[amountOfObjects];
        rangeStartTime        = new float[amountOfObjects];
        
        for (int i = 0; i < amountOfObjects; i++)
        {
            //Create the first set of positions
            previousPositionList[i]  = Random.insideUnitSphere * areaRadius;
            nextPositionList[i]      = Random.insideUnitSphere * areaRadius;
            //Create the lights at the given positions
            movingObjectList[i]      = GameObject.Instantiate<GameObject>(movingObjects[Random.Range(0, movingObjects.Length - 1)], originPosition + previousPositionList[i], this.transform.rotation);

            //Create the first set of random intensities and set it
            previousIntensityList[i] = Random.Range(minIntensity, maxIntensity);
            nextIntensityList[i] = Random.Range(minIntensity, maxIntensity);
            movingObjectList[i].GetComponent<Light>().intensity = previousIntensityList[i];

            //Create the first set of random ranges and set it
            previousRangeList[i] = Random.Range(minRange, maxRange);
            nextRangeList[i] = Random.Range(minRange, maxRange);
            movingObjectList[i].GetComponent<Light>().range = previousRangeList[i];
        }
    }
    
    void FixedUpdate()
    {
        for (int i = 0; i < amountOfObjects; i++)
        {
            ////Random Movement
            //Get the distance from the light to its target position
            float distanceLeft = Vector3.Distance(movingObjectList[i].transform.position - originPosition, nextPositionList[i]);
            //Optional visual representation of the path
            //Debug.DrawLine(movingObjectList[i].transform.position, nextPositionList[i] + originPosition);

            //Get the rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(nextPositionList[i] - (movingObjectList[i].transform.position - originPosition));

            //Rotate towards the required rotation by Slerping
            movingObjectList[i].transform.rotation = Quaternion.Slerp(movingObjectList[i].transform.rotation, targetRotation, slerpSpeed);

            //Move forward at the given speed
            movingObjectList[i].transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            //If the distance/margin is reached, create a new random destination
            if (distanceLeft < positionMargin)
            {
                previousPositionList[i] = nextPositionList[i];
                nextPositionList[i] = Random.insideUnitSphere * areaRadius;
            }

            ////Random Intensity and Range
            //Lerp values towards their target (see above)
            intensityLerp[i] = (Time.time - intensityStartTime[i]) / intensityChangeDuration;
            rangeLerp[i]     = (Time.time - rangeStartTime[i]) / rangeChangeDuration;
            movingObjectList[i].GetComponent<Light>().intensity = Mathf.Lerp(previousIntensityList[i], nextIntensityList[i], intensityLerp[i]);
            movingObjectList[i].GetComponent<Light>().range     = Mathf.Lerp(previousRangeList[i], nextRangeList[i], rangeLerp[i]);

            //Create new random values if target ones are reached
            if (intensityLerp[i] > 1)
            {
                previousIntensityList[i] = nextIntensityList[i];
                nextIntensityList[i] = Random.Range(minIntensity, maxIntensity);
                intensityStartTime[i] = Time.time;
            }       

            //Create new random values if target ones are reached
            if (rangeLerp[i] > 1)
            {
                previousRangeList[i] = nextRangeList[i];
                nextRangeList[i] = Random.Range(minRange, maxRange);
                rangeStartTime[i] = Time.time;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        //Visual aid to display range in editor
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
