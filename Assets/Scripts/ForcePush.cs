using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {

    public float radius;
    public Vector3 pushVector;

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
        RaycastHit[] hits = Physics.SphereCastAll(gameObject.transform.position, radius, transform.forward);
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.GetComponent<Rigidbody>() != null && hit.transform.tag != "Player")
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(pushVector, ForceMode.Impulse);
                Debug.DrawRay(gameObject.transform.position, hit.transform.position - gameObject.transform.position, Color.red, 5F);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward, radius);
    }
}
