using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dBox;
    public Text dText;

    public bool dActive;

    public string[] dialogueLines;
    public int currentLine;

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

		if(dActive && Input.GetKeyUp(KeyCode.Space))
        {
            //dBox.SetActive(false);
            //dActive = false;

            currentLine++;
        }

        if(currentLine >= dialogueLines.Length)
        {
            dBox.SetActive(false);
            dActive = false;

            currentLine = 0;
            player.canMove = true;
        }

        dText.text = dialogueLines[currentLine];
    }

    public void ShowDBox(string dialogue)
    {
        dActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }

    public void ShowDialogue()
    {
        dActive = true;
        dBox.SetActive(true);
        player.canMove = false;
        
    }
}
