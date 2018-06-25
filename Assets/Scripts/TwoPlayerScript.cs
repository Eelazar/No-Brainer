using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerScript : MonoBehaviour
{
    public bool fourDirectional;
    public float speed, jumpCDFloat, maxJumpLength;
    public Vector3 minJumpVector, jumpVectorIncrease;

    private GameObject objInFront;
    private float xInput, zInput;
    [HideInInspector]
    public Vector3 velocity, jumpChargeVector;
    private bool gridSwitchX, gridSwitchZ, jumpCDBool, jumpCharging;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TranslateInput();

    }

    void TranslateInput()
    {
        //Movement
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        if (fourDirectional)
        {
            if (xInput != 0 && zInput == 0) velocity.x = xInput;
            else if (zInput != 0 && xInput == 0) velocity.z = zInput;
            if (xInput == 0) velocity.x = 0;
            if (zInput == 0) velocity.z = 0;

            if (xInput > 0) transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            else if (xInput < 0) transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            else if (zInput > 0) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            else if (zInput < 0) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            velocity.x = xInput;
            velocity.z = zInput;
        }


        // transform.Translate(velocity.normalized * speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(transform.position + velocity.normalized * speed);

        //Jumping
        if (Input.GetButtonDown("Jump") && jumpCDBool == false)
        {
            jumpChargeVector = minJumpVector;
        }
        if (Input.GetButton("Jump") && jumpCDBool == false)
        {
            jumpCharging = true;
            Jump();
        }
        else if (Input.GetButtonUp("Jump") && jumpCharging == true)
        {
            jumpCharging = false;
            Jump();
        }

        //Interaction
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    void Jump()
    {
        if (jumpCharging == true)
        {
            jumpChargeVector += jumpVectorIncrease;
            jumpChargeVector = Vector3.ClampMagnitude(jumpChargeVector, maxJumpLength);
        }
        else
        {
            transform.GetComponent<Rigidbody>().AddRelativeForce(jumpChargeVector, ForceMode.VelocityChange);
            StartCoroutine(JumpCooldown());
        }
    }

    IEnumerator JumpCooldown()
    {
        jumpCDBool = true;
        yield return new WaitForSeconds(jumpCDFloat);
        jumpCDBool = false;
    }

    void Interact()
    {
        if (objInFront != null)
        {
            if (objInFront.GetComponent<InteractDialogue>() != null)
            {
                StartCoroutine(objInFront.GetComponent<InteractDialogue>().Trigger());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        objInFront = other.gameObject;
    }

}
