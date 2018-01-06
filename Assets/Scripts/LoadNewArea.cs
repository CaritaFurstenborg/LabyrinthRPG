using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad; // name of the scene to load

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // if something enters trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player") // check if the object colling is the player
        {
            SceneManager.LoadScene(levelToLoad); // load the selected scene
        }
    }
}
