using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed;
    public Vector3 jumpVector;

    private float xInput, zInput;
    public Vector3 velocity;
    private bool gridSwitchX, gridSwitchZ;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        TranslateInput();
        
    }

    void TranslateInput()
    {
        //Movement
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        if (xInput != 0 && zInput == 0) velocity.x = xInput;
        else if (zInput != 0 && xInput == 0) velocity.z = zInput;
        if (xInput == 0) velocity.x = 0;
        if (zInput == 0) velocity.z = 0;

        if (xInput > 0) transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        else if (xInput < 0) transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        else if (zInput > 0) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (zInput < 0) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));


        // transform.Translate(velocity.normalized * speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(transform.position + velocity.normalized * speed);

        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.Impulse);
        }
    }
}
