using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour {

    public List<int> availableQuestID = new List<int>();
    public List<int> receivableQuestID = new List<int>();

    public GameObject questMarker;
    public Image questStateImage;

    public Sprite questAvailableSprite;
    public Sprite questReceivableSprite;
    public Sprite questActiveSprite;

    private bool intTrigger = false;

    private NpcMovement npcMove;

    // Use this for initialization
    void Start () {
        if(gameObject.GetComponent<NpcMovement>() != null) // check if questgiver is NPC
        {
            npcMove = gameObject.GetComponent<NpcMovement>();
        }        
        SetQuestMarker();
	}
	
	// Update is called once per frame
	void Update () {
        SetQuestMarker();
		if(intTrigger && Input.GetKeyDown(KeyCode.K))
        {
            //quest ui manager            
            QuestUIManager.uiManagerQ.CheckQuests(this);

            //set NPC to stay put while quest panel is open
            if (npcMove != null)
            {
                npcMove.canMove = false;
            }
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            intTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            intTrigger = false;
        }
    }

    public void SetQuestMarker()
    {
        if(QuestManager.questManager.CheckCompletedQuests(this))
        {
            questMarker.SetActive(true);
            questStateImage.sprite = questReceivableSprite;
        }
        else if(QuestManager.questManager.CheckAvailableQuests(this))
        {
            questMarker.SetActive(true);
            questStateImage.sprite = questAvailableSprite;
        }
        else if(QuestManager.questManager.CheckAcceptedQuests(this))
        {
            questMarker.SetActive(true);
            questStateImage.sprite = questActiveSprite;
        }
        else
        {
            questMarker.SetActive(false);
        }
    }
}
