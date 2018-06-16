using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {

    public float radius, arcSize;
    public Vector3 direction, pushVector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Push();
        }
	}

    void Push()
    {
        RaycastHit[] hits = Physics.SphereCastAll(gameObject.transform.position, radius, direction);
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.GetComponent<Rigidbody>() != null)
            {
                Vector3 v1 = (hit.transform.position - gameObject.transform.position).normalized;
                if (Vector3.Dot(v1, gameObject.transform.forward) > arcSize)
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce(pushVector, ForceMode.Impulse);
                }
            }
        }
    }
}
