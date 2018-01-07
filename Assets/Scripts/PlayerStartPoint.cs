using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController player;
    private CameraController camera;

    public string pointName;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

        if(player.startPoint == pointName)
        {
            player.transform.position = transform.position;

            camera = FindObjectOfType<CameraController>();
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
