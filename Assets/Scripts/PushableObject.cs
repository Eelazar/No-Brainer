using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour {

    public float pushForce, winMin, winMax;
    public float start, end, pushBack;
    public string axis;
    private bool win;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(axis == "x")
        {
            if(transform.position.x < winMax && transform.position.x > winMin)
            {
                win = true;
            }
        }
        else if(axis == "z")
        {
            if (transform.position.z < winMax && transform.position.z > winMin)
            {
                win = true;
            }
        }

		if(axis == "x")
        {
            if(gameObject.transform.position.x >= end)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.transform.position = (gameObject.transform.position + new Vector3(-pushBack, 0, 0));
            }
            else if(gameObject.transform.position.x <= start)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.transform.position = (gameObject.transform.position + new Vector3(pushBack, 0, 0));
            } 
        }
        else if(axis == "z")
        {
            if (gameObject.transform.position.z >= end)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.transform.position = (gameObject.transform.position + new Vector3(0, 0, -pushBack));
            }
            else if (gameObject.transform.position.z <= start)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.transform.position = (gameObject.transform.position + new Vector3(0, 0, pushBack));
            }
        }

	}

    public void Push(Transform player)
    {
        if(axis == "x")
        {
            Vector3 v = gameObject.transform.position - player.position;
            v.Normalize();
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(pushForce * v.x, 0, 0), ForceMode.Impulse);
            Debug.Log(new Vector3(pushForce * v.x, 0, 0));            
        }
        else if (axis == "z")
        {
            Vector3 v = gameObject.transform.position - player.position;
            v.Normalize();
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, pushForce * v.z), ForceMode.Impulse);
            Debug.Log(new Vector3(0, 0, pushForce * v.z));
        }
    }
}
