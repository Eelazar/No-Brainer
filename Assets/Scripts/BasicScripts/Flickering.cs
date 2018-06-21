using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour {

    private Light thisLight;
    private float amountOfFlickers, defaultIntensity;
    private bool flickering;
    

    // Use this for initialization
    void Start ()
    {
        thisLight = gameObject.GetComponent<Light>();
        defaultIntensity = thisLight.intensity;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!flickering)
        {
            StartCoroutine(Flicker());
            Debug.Log("Start Flicker");
        }
	}
    

    IEnumerator Flicker()
    {
        flickering = true;
        amountOfFlickers = Random.Range(3, 7);
        yield return new WaitForSeconds(Random.Range(3, 15));

        for(int i = 0; i < amountOfFlickers; i++)
        {
            thisLight.intensity = Random.Range(0, defaultIntensity/2);
            yield return new WaitForSeconds(Random.Range(0, 0.1F));
            thisLight.intensity = defaultIntensity;
        }

        thisLight.intensity = defaultIntensity;
        flickering = false;
    }
}
