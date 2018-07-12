using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFactory : MonoBehaviour {

    [Header("Dimensions")]
    [Tooltip("Size of the bounding box (always twice the extents)")]
    public Vector3 size;

    [Header("Fish Attributes")]
    [Tooltip("Fish prefabs you want to use, will be chosen randomly upon creation")]
    public GameObject[] fishPrefabs;
    [Tooltip("Amount of fishes you want spawned")]
    public int amountOfFishes;

    //The Bounding Box the fishes will be swimming around in
    private Bounds fishArea;
    


	void Start ()
    {
        //Create the Bounding Box
        fishArea = new Bounds(this.transform.position, size);

        //Spawn the chosen amount of fishes at random positions, and assign their area
        for(int i = 0; i < amountOfFishes; i++)
        {
            int fishIndex = Random.Range(0, fishPrefabs.Length - 1);
            GameObject fish = GameObject.Instantiate<GameObject>(fishPrefabs[fishIndex], PickRandomPosition(), fishPrefabs[fishIndex].transform.rotation);
            fish.GetComponent<Fish>().swimArea = fishArea;
        }
	}

    Vector3 PickRandomPosition()
    {
        Vector3 position;
        position.x = Random.Range(fishArea.min.x, fishArea.max.x);
        position.y = Random.Range(fishArea.min.y, fishArea.max.y);
        position.z = Random.Range(fishArea.min.z, fishArea.max.z);        

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
