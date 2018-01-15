using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour {

    public float toastTime;
    public float displayTime;

    public GameObject hintBox;
    public Text hintText;

    public bool hintActive;

	// Use this for initialization
	void Start () {
        displayTime = toastTime;
    }
	
	// Update is called once per frame
	void Update () {
		if(hintActive)
        {
            displayTime -= Time.deltaTime;
            if(displayTime <= 0)
            {
                gameObject.SetActive(false);
                hintActive = false;
                displayTime = toastTime;
            }
        }
	}    
}
