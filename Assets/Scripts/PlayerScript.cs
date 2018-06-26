using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Controls")]
    public bool fourDirectional;
    public float speed, jumpCDFloat, maxJumpLength;
    public Vector3 minJumpVector, jumpVectorIncrease;

    [Header("Other")]
    public Transform spawnPoint;

    [Header("Temporary")]
    public GameObject sibling;

    private GameObject currentInteractionTarget;
    private float xInput, zInput;
    [HideInInspector]
    public Vector3 velocity, jumpChargeVector;
    private bool jumpCDBool, jumpCharging;

    // Use this for initialization
    void Start()
    {
        //Set the player position to the spawn position
        transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        TranslateInput();
    }

    void TranslateInput()
    {
        //////Movement//////
        //Get the player Input
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        //Check if movement is four directional 
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
        //Translate the input into movement
        GetComponent<Rigidbody>().MovePosition(transform.position + velocity.normalized * speed);

        //////Jumping//////
        if(Input.GetButtonDown("Jump") && jumpCDBool == false)
        {
            //Set the minimum amnount of force as soon as the buton is pressed
            jumpChargeVector = minJumpVector;
        }
        if (Input.GetButton("Jump") && jumpCDBool == false)
        {
            //Keep adding force as long as the button is held down
            jumpCharging = true;
            Jump();
        }
        else if(Input.GetButtonUp("Jump") && jumpCharging == true)
        {
            //Release the charge when the button is released
            jumpCharging = false;
            Jump();
        }

        //////Interaction//////
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    void Jump()
    {
        //If the player is charging add force to the final vector
        if(jumpCharging == true)
        {
            jumpChargeVector += jumpVectorIncrease;
            jumpChargeVector = Vector3.ClampMagnitude(jumpChargeVector, maxJumpLength);
        }
        //If he releases the button exert the final force and set the jump on cooldown
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
        if(currentInteractionTarget != null)
        {
            if (currentInteractionTarget.GetComponent<InteractDialogue>() != null)
            {
                StartCoroutine(currentInteractionTarget.GetComponent<InteractDialogue>().Trigger());
            }
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        //Set the current object you're viewing to the collider object if it is tagged as Interactable
        if(other.tag == "Interactable")
        {
            currentInteractionTarget = other.gameObject;
        }

        //Set spawn position to collider position if collider is tagged as SavePoint
        if(other.tag == "SavePoint")
        {
            spawnPoint = other.transform;
        }

        //Respawn the player if he collides with a deadly object
        if(other.tag == "Deadly")
        {
            ReSpawn();
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Remove the current object you're viewing if it left your collider
        if (other.gameObject == currentInteractionTarget)
        {
            currentInteractionTarget = null;
        }
    }

    void ReSpawn()
    {
        gameObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        sibling.transform.SetPositionAndRotation(sibling.GetComponent<PlayerScript>().spawnPoint.position, sibling.GetComponent<PlayerScript>().spawnPoint.rotation);
    }    
}
