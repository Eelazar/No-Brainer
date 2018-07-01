using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour {

    public enum Axis { x, z }

    [Header("Movement")]
    public Axis slideAxis;
    public float pushForce;
    public float minPosition;
    public float maxPosition;

    [Header("Win Condition")]
    public float winMin;
    public float winMax;

    [Header("Big Picture")]
    public GameObject puzzleMaster;
    [Tooltip("Must start at 0")]
    public int puzzleIndex;

    [SerializeField]
    private bool win;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(slideAxis == Axis.x)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPosition, maxPosition), transform.position.y, transform.position.z);
        }
        else if (slideAxis == Axis.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, minPosition, maxPosition));
        }
         
        if (win==true)
        {
            puzzleMaster.GetComponent<PushMaster>().puzzlePieces[puzzleIndex] = true;
        }

        if(slideAxis == Axis.x)
        {
            if(transform.position.x < winMax && transform.position.x > winMin)
            {
                win = true;
            }
        }
        else if(slideAxis == Axis.z)
        {
            if (transform.position.z < winMax && transform.position.z > winMin)
            {
                win = true;
            }
        }
        
	} 

    public void Push(Transform player)
    {
        if(slideAxis == Axis.x)
        {
            Vector3 direction = gameObject.transform.position - player.position;
            direction.Normalize();
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(pushForce * direction.x, 0, 0), ForceMode.Impulse);
        }
        else if (slideAxis == Axis.z)
        {
            Vector3 direction = gameObject.transform.position - player.position;
            direction.Normalize();
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, pushForce * direction.z), ForceMode.Impulse);
        }
    }
}
