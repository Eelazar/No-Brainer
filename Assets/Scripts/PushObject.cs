using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour {

    public string axis;
    public float speed, directionCutoff, pushBack;
    public float start, end;

    private bool trigger;
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (trigger)
        {
            Vector3 direction = gameObject.transform.position - player.transform.position;
            if(axis == "x")
            {
                if(gameObject.transform.position.x > start && gameObject.transform.position.x < end)
                {
                    if (direction.normalized.x < directionCutoff)
                    {
                        gameObject.transform.Translate(new Vector3(-speed, 0));
                    }
                    else if (direction.normalized.x > directionCutoff)
                    {
                        gameObject.transform.Translate(new Vector3(speed, 0));
                    }
                }
                else if(gameObject.transform.position.x < start)
                {
                    gameObject.transform.Translate(new Vector3(pushBack, 0, 0));
                }
                else if(gameObject.transform.position.x > end)
                {
                    gameObject.transform.Translate(new Vector3(-pushBack, 0, 0));
                }
            }
            else if(axis == "z")
            {
                if(gameObject.transform.position.z > start && gameObject.transform.position.z < end)
                {
                    if (direction.normalized.z < directionCutoff)
                    {
                        gameObject.transform.Translate(new Vector3(0, 0, -speed));
                    }
                    else if (direction.normalized.z > directionCutoff)
                    {
                        gameObject.transform.Translate(new Vector3(0, 0, speed));
                    }
                }
                else if (gameObject.transform.position.z < start)
                {
                    gameObject.transform.Translate(new Vector3(0, 0, pushBack));
                }
                else if (gameObject.transform.position.z > end)
                {
                    gameObject.transform.Translate(new Vector3(0, 0, -pushBack));
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            trigger = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = false;
        }
    }
}
