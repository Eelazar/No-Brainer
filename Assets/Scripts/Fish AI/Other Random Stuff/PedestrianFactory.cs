using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianFactory : MonoBehaviour {

    [Header("Object Attributes")]
    [Tooltip("Objects you want to spawn, will be chosen randomly upon creation")]
    public GameObject[] movingObjects;
    [Tooltip("Amount of objects you want to spawn")]
    public int amountOfObjects;

    [Header("Dimensions")]
    [Tooltip("Size of the bounding box (always twice the extents)")]
    public Vector3 size;


    //The Bounding Box the pedestrians will be moving around in
    private Bounds pedestrianArea;

    //Variables
    private GameObject[] movingObjectList;


    void Start()
    {
        //Initialize stuff
        pedestrianArea = new Bounds(this.transform.position, size);
        movingObjectList = new GameObject[amountOfObjects];

        //Create the first set of random values
        for(int i = 0; i < amountOfObjects; i++)
        {
            int j = Random.Range(0, movingObjects.Length - 1);
            movingObjectList[i] = GameObject.Instantiate<GameObject>(movingObjects[j], FindNewDestination(), movingObjects[j].transform.rotation);
            movingObjectList[i].GetComponent<Pedestrian>().moveArea = pedestrianArea;
        }
    }
    
    Vector3 FindNewDestination()
    {
        Vector3 position;
        position.x = Random.Range(pedestrianArea.min.x, pedestrianArea.max.x);
        position.y = transform.position.y;
        position.z = Random.Range(pedestrianArea.min.z, pedestrianArea.max.z);

        return position;
    }

    private void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, size);
#endif

    }
}
