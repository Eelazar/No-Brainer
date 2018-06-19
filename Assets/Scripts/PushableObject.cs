using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour {

    public float pushForce;
    public float start, end, pushBack;
    public string axis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(axis == "x")
        {
            if(gameObject.transform.position.x > end)
            {
                gameObject.transform.Translate(new Vector3(-pushBack, 0, 0));
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else if(gameObject.transform.position.x < start)
            {
                gameObject.transform.Translate(new Vector3(pushBack, 0, 0));
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        else if(axis == "z")
        {
            if (gameObject.transform.position.z > end)
            {
                gameObject.transform.Translate(new Vector3(0, 0, -pushBack));
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else if (gameObject.transform.position.z < start)
            {
                gameObject.transform.Translate(new Vector3(0, 0, pushBack));
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

	}

    public void Push(Transform player)
    {
        if(axis == "x")
        {
            Vector3 v = gameObject.transform.position - player.position;
            v.Normalize();
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(pushForce * v.x, 0, 0));
        }
        else if (axis == "z")
        {
            Vector3 v = gameObject.transform.position - player.position;
            v.Normalize();
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, pushForce * v.z));
        }
    }
}
