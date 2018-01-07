using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad; // name of the scene to load

    public string exitPoint;

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
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
            player.startPoint = exitPoint;
        }
    }
}
