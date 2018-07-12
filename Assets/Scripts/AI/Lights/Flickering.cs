using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour {

    [Tooltip("Minimum flicker intensity (Random)")]
    public float minFlickerIntensity;
    [Tooltip("Maximum flicker intensity (Random)")]
    public float maxFlickerIntensity;
    [Tooltip("Minimum flicker duration (Random)")]
    public float minFlickerDuration;
    [Tooltip("Maximum flicker duration (Random)")]
    public float maxFlickerDuration;
    [Tooltip("Minimum amount of flickers (Random)")]
    public float minFlickerAmount;
    [Tooltip("Maximum amount of flickers (Random)")]
    public float maxFlickerAmount;
    [Tooltip("Minimum wait time between flickers (Random)")]
    public float minWait;
    [Tooltip("Maximum wait time between flickers (Random)")]
    public float maxWait;


    private Light thisLight;
    private float defaultIntensity;
    private bool flickering;
    
    
    void Start ()
    {
        //Get the light
        thisLight = gameObject.GetComponent<Light>();

        //Remember the starting intensity
        defaultIntensity = thisLight.intensity;
    }
	
	void Update ()
    {
        //If the light isn't flickering, flicker
        if (!flickering)
        {
            StartCoroutine(Flicker());
        }
	}
    

    IEnumerator Flicker()
    {
        flickering = true;

        //Generate a random amnount of flickers between the given limits
        float amountOfFlickers = Random.Range(minFlickerAmount, maxFlickerAmount);

        //Wait for a random amount of time between the given limits
        yield return new WaitForSeconds(Random.Range(minWait, maxWait));

        for(int i = 0; i < amountOfFlickers; i++)
        {
            //Set the intensity to a random value between the given limits
            thisLight.intensity = Random.Range(minFlickerIntensity, maxFlickerIntensity);

            //Wait for a random amount of time between the given limits
            yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));

            //Reset the intensity
            thisLight.intensity = defaultIntensity;
        }

        //Reset the intensity
        thisLight.intensity = defaultIntensity;
        flickering = false;
    }
}
