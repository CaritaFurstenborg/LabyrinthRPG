using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour {

    public string hintToDisplay;    

    private HintManager hMan;

	// Use this for initialization
	void Start () {
        hMan = GameObject.Find("CanvasUiManager").transform.Find("HintManager").GetComponent<HintManager>();
        if(hMan == null)
        {
            Debug.Log("Can not find HINTMANAGER!");
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            hMan.hintText.text = hintToDisplay;
            hMan.hintBox.SetActive(true);
            hMan.hintActive = true;
        }        
    }
}
