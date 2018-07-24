using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The speed multiplier for basic movement")]
    public float speed;
    [Tooltip("The amount of time it takes to rotate the player once")]
    [Range(0, 0.2F)]
    public float turnDuration;

    [Header("Jump")]
    public bool enableJump;
    [Tooltip("The force applied to the player when jumping")]
    public Vector3 jumpVector;
	public float minDistanceToFloor = 1F;

    [Header("Other")]
    [Tooltip("The starting position of the player when first starting the scene, will automatically get saved to PlayerPrefs upon loading")]
    public Vector3 sceneSpawn;
    public Animator animator;

    [Header("Sounds")]
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip dieSound;

    public bool enableWalkSound;

    
    ////Variables
    //Movement
    private float xInput, zInput;
    private Vector3 velocity;
    private bool canJump;
    //Rotating
    private bool rotating;
    private Vector3 currentRotation;
    private Vector3 rightRotation   = new Vector3(0, 90, 0);
    private Vector3 leftRotation    = new Vector3(0, -90, 0);
    private Vector3 forwardRotation = new Vector3(0, 0, 0);
    private Vector3 backRotation    = new Vector3(0, 180, 0);
    
    //Sound
    private AudioSource source;
    private AudioSource sourceOneShot;
    [HideInInspector]
    public bool walking;
    //Animator
  
    

    void Start()
    {
        source = this.GetComponents<AudioSource>()[0];
        sourceOneShot = this.GetComponents<AudioSource>()[1];


        //Reset amount of deaths
        PlayerPrefs.SetFloat("Deaths", 0);
        
        if(sceneSpawn != Vector3.zero)
        {
            transform.position = sceneSpawn;
        }
        else
        {
            transform.position = GetSpawn();
        }
        currentRotation = forwardRotation;
    }

    void LateUpdate()
    {
        //Get the players velocity, and set the default to not walking
        Vector3 v = GetComponent<Rigidbody>().velocity;
        walking = false;

        //If the player is moving in either direction, set walking to true
        if (velocity.x != 0 || velocity.z != 0)
        {
            walking = true;
        }
        //But if he is in the air, set it to false again
        if(v.y > 0.5F || v.y < -0.5F)
        {
            walking = false;
        }

        //Finally, play the walking sound and animation if he is walking only
        if (walking && !source.isPlaying)
        {
            animator.SetBool("moving", true);
            source.clip = walkSound;
            if (enableWalkSound) source.Play();
        }
        else if(walking == false)
        {
            animator.SetBool("moving", false);
            if (enableWalkSound)  source.Stop();
        }
    }

    void Update()
    {
        GetInput();
        if (enableJump) TranslateJumpInput();
    }

    void FixedUpdate()
    {
        TranslateMovementInput();
    }

    void GetInput()
    {
        //Get the player Input
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }

    void TranslateMovementInput()
    {
        //Assign the input to the velocity vector
        if (xInput != 0 && zInput == 0)
        {
            velocity.x = xInput;
        }
        else if (zInput != 0 && xInput == 0)
        {
            velocity.z = zInput;
        }

        //If the player isn't moving set his velocity to 0
        if (xInput == 0)
        {
            velocity.x = 0;
        }
        if (zInput == 0)
        {
            velocity.z = 0;
        }

        //Rotate the player according to the input
        if(rotating == false)
        {
            if (xInput > 0)
            {
                if (currentRotation != rightRotation)
                {
                    StartCoroutine(RotatePlayer(new Vector3(0, 90, 0)));
                }
            }
            else if (xInput < 0)
            {
                if (currentRotation != leftRotation)
                {
                    StartCoroutine(RotatePlayer(new Vector3(0, -90, 0)));
                }
            }
            else if (zInput > 0)
            {
                if (currentRotation != forwardRotation)
                {
                    StartCoroutine(RotatePlayer(new Vector3(0, 0, 0)));
                }
            }
            else if (zInput < 0)
            {
                if (currentRotation != backRotation)
                {
                    StartCoroutine(RotatePlayer(new Vector3(0, 180, 0)));
                }
            }
        }

        //Multiply the direction by the chosen speed
        velocity.Normalize();
        velocity *= speed;

        //Translate the final input into movement
        GetComponent<Rigidbody>().MovePosition(transform.position + velocity * Time.deltaTime);        
    }

    void TranslateJumpInput()
    {
        RaycastHit hit;
        canJump = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, minDistanceToFloor);
        if (Input.GetButtonDown("Jump"))
        {
            // Does the ray intersect any object
            if (canJump)
            {
                sourceOneShot.PlayOneShot(jumpSound);
                transform.GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.VelocityChange);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Respawn the player if he collides with a deadly object
        if(other.tag == "Deadly")
        {
            Respawn();
        }
    }
    

    //Reset the players position
    public void Respawn()
    {
        //Record amount of deaths
        float f  = PlayerPrefs.GetFloat("Deaths");
        float ff = PlayerPrefs.GetFloat("TotalDeaths");
        PlayerPrefs.SetFloat("Deaths", f += 1);
        PlayerPrefs.SetFloat("TotalDeaths", ff += 1);

        //Death sound and... death
        sourceOneShot.PlayOneShot(dieSound);
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
        rotating = true;
        Quaternion start = transform.rotation;
        Quaternion end = Quaternion.Euler(targetVector);
        float startTime = Time.realtimeSinceStartup;
        float t = 0;

        while(t < 1)
        {
            t = (Time.realtimeSinceStartup - startTime) / turnDuration;
            transform.rotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }

        currentRotation = targetVector;
        rotating = false;
        yield return null;
    }
}
