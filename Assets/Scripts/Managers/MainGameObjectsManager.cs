using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameObjectsManager : MonoBehaviour {

    public static MainGameObjectsManager instance;
    
    private static bool exists;

    // Use this for initialization
    void Start () {

		if(!exists)
        {
            instance = this;
            exists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestroyMainGameObjects()
    {
        exists = false;
        Destroy(gameObject);
    }
}
