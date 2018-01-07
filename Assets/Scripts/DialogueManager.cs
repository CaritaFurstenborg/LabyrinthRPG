using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dBox;
    public Text dText;

    public bool dActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(dActive && Input.GetKeyDown(KeyCode.Space))
        {
            dBox.SetActive(false);
            dActive = false;
        }
	}

    public void ShowDBox(string dialogue)
    {
        dActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }
}
