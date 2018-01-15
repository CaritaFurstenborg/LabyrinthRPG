using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for any characters to use
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

    protected Vector2 direction; // inheriting objects can access

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    // protected virtual allows to override the function in inheriting classes
	protected virtual void Update () {
        Move();
	}

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
