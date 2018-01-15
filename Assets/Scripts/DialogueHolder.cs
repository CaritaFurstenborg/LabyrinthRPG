using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {

    public string dialogue;
    public DialogueManager dm;

    public string[] dialogueLines;

	// Use this for initialization
	void Start () {
        dm = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if(!dm.dActive)
            {
                dm.dialogueLines = dialogueLines;
                dm.currentLine = 0;
                dm.ShowDialogue();
            }

            /*if(Input.GetKeyUp(KeyCode.Space))
            {
                //dm.ShowDBox(dialogue);

                if(!dm.dActive)
                {
                    dm.dialogueLines = dialogueLines;
                    dm.currentLine = 0;
                    dm.ShowDialogue();
                }

                if(transform.parent.GetComponent<NpcMovement>() != null)
                {
                    transform.parent.GetComponent<NpcMovement>().canMove = false;
                }
            }*/
        }
    }
}
