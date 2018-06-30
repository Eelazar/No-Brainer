using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour {

    public GameObject siblingGO, player;
    public float creationWaitTime;
    public int animationSteps;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if(GUI.Button(new Rect(50, 50, 100, 20), "Sibling")){
            StartCoroutine(CreateSibling());
        }
    }

    IEnumerator CreateSibling()
    {
        GameObject sibling = GameObject.Instantiate<GameObject>(siblingGO);
        sibling.transform.parent = null;
        sibling.transform.position = player.transform.position;
        yield return new WaitForSeconds(creationWaitTime);

        sibling.transform.localScale += new Vector3(0, 0, 0.1F);
        yield return new WaitForSeconds(creationWaitTime);

        for(int i = 0; i < animationSteps; i++)
        {            
            sibling.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(-90, 0, i / animationSteps), 0, 0));
            yield return new WaitForSeconds(0.05F);
        }
        yield return new WaitForSeconds(creationWaitTime);

        for(int i = 0; i < animationSteps; i++)
        {
            sibling.transform.localScale += new Vector3(0, 0, Mathf.Lerp(0.1F, sibling.transform.localScale.x, animationSteps * (i / animationSteps)));
            yield return new WaitForSeconds(0.05F);
        }
        yield return new WaitForSeconds(creationWaitTime);

        yield return null;
    }
}
