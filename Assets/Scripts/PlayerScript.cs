using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The speed multiplier for basic movement")]
    public float speed;
    [Tooltip("The amount of time it takes to rotate the player once")]
    public float turnDuration;

    [Header("Jump")]
    public float jumpCDFloat; 
    public float maxJumpLength;
    public Vector3 minJumpVector;
    public Vector3 jumpVectorIncrease;

    [Header("Other")]
    [Tooltip("The starting position of the player when first starting the scene, will automatically get saved to PlayerPrefs upon loading")]
    public Vector3 sceneSpawn; 
    
    ////Variables
    //Movement
    private float xInput, zInput;
    private Vector3 velocity;
    private bool rotating;
    //Jumping
    private bool jumpCDBool, jumpCharging;
    private Vector3 jumpChargeVector;
    

    void Start()
    {
        //Set the player position to the scene spawn position and save it to the PlayerPrefs
        transform.position = sceneSpawn;
        SetSpawn(sceneSpawn);
    }
    
    void FixedUpdate()
    {
        TranslateMovementInput();
        TranslateJumpInput();
    }

    void TranslateMovementInput()
    {
        //Get the player Input
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        //Assign the input to the velocity vector
        if (xInput != 0 && zInput == 0) velocity.x = xInput;
        else if (zInput != 0 && xInput == 0) velocity.z = zInput;

        //If the player isn't moving set his velocity to 0
        if (xInput == 0) velocity.x = 0;
        if (zInput == 0) velocity.z = 0;

        //Rotate the player according to the input
        if(rotating == false)
        {
            if (xInput > 0) StartCoroutine(RotatePlayer(new Vector3(0, 90, 0)));
            else if (xInput < 0) StartCoroutine(RotatePlayer(new Vector3(0, -90, 0)));
            else if (zInput > 0) StartCoroutine(RotatePlayer(new Vector3(0, 0, 0)));
            else if (zInput < 0) StartCoroutine(RotatePlayer(new Vector3(0, 180, 0)));
        }

        //Multiply the direction by the chosen speed
        velocity.Normalize();
        velocity *= speed;

        //Translate the final input into movement
        GetComponent<Rigidbody>().MovePosition(transform.position + velocity * Time.deltaTime);        
    }

    void TranslateJumpInput()
    {
        ///////////////////////////////////////////////////
        //NOT FINAL; WAITING ON JUMPING LEVEL TO FINALIZE//
        ///////////////////////////////////////////////////

        if (Input.GetButtonDown("Jump") && jumpCDBool == false)
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
        else if (Input.GetButtonUp("Jump") && jumpCharging == true)
        {
            //Release the charge when the button is released
            jumpCharging = false;
            Jump();
        }
    }

    void Jump()
    {
        ///////////////////////////////////////////////////
        //NOT FINAL; WAITING ON JUMPING LEVEL TO FINALIZE//
        ///////////////////////////////////////////////////

        //If the player is charging add force to the final vector
        if (jumpCharging == true)
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
        ///////////////////////////////////////////////////
        //NOT FINAL; WAITING ON JUMPING LEVEL TO FINALIZE//
        ///////////////////////////////////////////////////

        jumpCDBool = true;
        yield return new WaitForSeconds(jumpCDFloat);
        jumpCDBool = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //Respawn the player if he collides with a deadly object
        if(other.tag == "Deadly")
        {
            Respawn();
        }
    }

    void OnTriggerExit(Collider other)
    {
    }

    //Reset the players position
    public void Respawn()
    {
        gameObject.transform.position = GetSpawn();
    }

    //Sets the spawn position to the Vector given as a parameter
    void SetSpawn(Vector3 spawn)
    {
        PlayerPrefs.SetFloat("xSpawn", spawn.x);
        PlayerPrefs.SetFloat("ySpawn", spawn.y);
        PlayerPrefs.SetFloat("zSpawn", spawn.z);
    }

    //Returns the saved spawn position, or Vector3.zero if it cannot be found
    Vector3 GetSpawn()
    {
        Vector3 spawn = new Vector3(PlayerPrefs.GetFloat("xSpawn", 0), PlayerPrefs.GetFloat("ySpawn", 0), PlayerPrefs.GetFloat("zSpawn", 0));
        return spawn;
    }

    IEnumerator RotatePlayer(Vector3 targetVector)
    {
        /*Debug.Log("turn");
        rotating = true;
        Quaternion start = transform.rotation;
        Quaternion end = Quaternion.Euler(targetVector);
        float startTime = Time.time;
        float t = 0;

        while(t < 1)
        {
            t = (Time.time - startTime) / turnDuration;
            transform.rotation = Quaternion.Slerp(start, end, t);
        }

        rotating = false;*/
        yield return null;
    }
}
