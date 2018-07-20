using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAnimation : MonoBehaviour {

    public Color color;
    public float fadeDuration;
    public AnimationCurve curve;


    private Image fadeImg;
    private float lerpStart;
    private float lerpT;


	void Start ()
    {
        fadeImg = GetComponent<Image>();
        fadeImg.color = color;
        GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

        lerpStart = Time.time;
	}
	
	void Update ()
    {
        lerpT = (Time.time - lerpStart) / fadeDuration;
        color.a = Mathf.Lerp(1, 0, curve.Evaluate(lerpT));
        fadeImg.color = color;

        if(fadeImg.color.a <= 0)
        {
            Destroy(gameObject);
        }
	}
}
