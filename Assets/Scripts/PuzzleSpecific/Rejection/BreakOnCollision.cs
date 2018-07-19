using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnCollision : MonoBehaviour {

    [Tooltip("Objects tagged with this tag will be able to break the object")]
    public string tagThatBreaks;
    [Tooltip("Amount of damage needed to fully break the object")]
    public float totalDamage;
    [Tooltip("Amount of damage dealt by one collision")]
    public float damage;
    [Header("Sounds")]
    public AudioClip damageSound;
    public AudioClip destroySound;


    private AudioSource source;
    private float damageCounter;
    private bool destroyed;

    
	void Start ()
    {
        source = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
		if(damageCounter >= totalDamage && destroyed == false)
        {
            destroyed = true;
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == tagThatBreaks)
        {
            source.PlayOneShot(damageSound);
            damageCounter += damage;
        }
    }
}
