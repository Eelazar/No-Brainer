using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public bool fourDirectional;
    public float speed;

    [Header("Jump")]
    public float jumpCDFloat; 
    public float maxJumpLength;
    public Vector3 minJumpVector, jumpVectorIncrease;

    [Header("Other")]
    public Vector3 sceneSpawn;

    [Header("Temporary")]
    public GameObject sibling;    

    private GameObject currentInteractionTarget;
    private float xInput, zInput;
    private bool jumpCDBool, jumpCharging;
    private Vector3 velocity, jumpChargeVector;

    // Use this for initialization
    void Start()
    {
        //Set the player position to the scene spawn position and save it to the PlayerPrefs
        transform.position = sceneSpawn;
        PlayerPrefs.SetFloat("xSpawn", sceneSpawn.x);
        PlayerPrefs.SetFloat("ySpawn", sceneSpawn.y);
        PlayerPrefs.SetFloat("zSpawn", sceneSpawn.z);
    }

    // Update is called once per frame
    void FixedUpdate()
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
        GetComponent<Rigidbody>().MovePosition(transform.position + velocity.normalized * speed * Time.deltaTime);
              

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
            else if (currentInteractionTarget.GetComponent<PushableObject>() != null)
            {
                currentInteractionTarget.GetComponent<PushableObject>().Push(transform);
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
        gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("xSpawn", 0), PlayerPrefs.GetFloat("ySpawn", 0), PlayerPrefs.GetFloat("zSpawn", 0));
        sibling.transform.position = sibling.GetComponent<PlayerScript>().sceneSpawn;
    }    
}
