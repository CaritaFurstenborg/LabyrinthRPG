using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRange : MonoBehaviour {

    private Enemy parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            parent.SetTarget(other.transform);        
        }
    }    
}
