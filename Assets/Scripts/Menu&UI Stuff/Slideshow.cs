using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour {

    public GameObject imageHolder;
    public Sprite[] imagesToShow;
    public Sprite fillerImage;
    public float waitBefore;
    public float waitBetween;
    public float waitAfter;
    public float showDuration;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StartSlideshow());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator StartSlideshow()
    {
        imageHolder.SetActive(true);
        yield return new WaitForSeconds(waitBefore);

        for(int i = 0; i < imagesToShow.Length; i++)
        {
            imageHolder.GetComponent<Image>().sprite = imagesToShow[i];
            yield return new WaitForSeconds(showDuration);
            imageHolder.GetComponent<Image>().sprite = fillerImage;
            yield return new WaitForSeconds(waitBetween);
        }

        yield return new WaitForSeconds(waitAfter);

        Destroy(imageHolder);
        Destroy(gameObject);
    }
}
