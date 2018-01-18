using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRange : MonoBehaviour {

    private kksController parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<kksController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            parent.target = other.gameObject.transform;
            if(parent.target != null)
            {
                Debug.Log("Target detected");
            }
            else
            {
                Debug.Log("NO Target found!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            parent.target = null;
            Debug.Log("Target lost.");
        }
    }
}
