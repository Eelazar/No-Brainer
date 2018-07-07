using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour {

    public GameObject obj;
    public float distance;
    public Vector3 grid;

    private float index;

	// Use this for initialization
	void Start ()
    {
        Vector3 spawn = Vector3.zero;

        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                for (int z = 0; z < grid.z; z++)
                {
                    spawn =  new Vector3(transform.position.x + (x * distance), transform.position.y + (y * distance), transform.position.z + (z * distance));
                    GameObject.Instantiate<GameObject>(obj, spawn, obj.transform.rotation);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
