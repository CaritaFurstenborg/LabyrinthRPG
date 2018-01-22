using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : NPC {

    [SerializeField]
    private CanvasGroup healthgroup;

    public List<int> availableQuestID = new List<int>();
    public List<int> receivableQuestID = new List<int>();

    public GameObject questMarker;
    public Image questStateImage;

    public Sprite questAvailableSprite;
    public Sprite questReceivableSprite;
    public Sprite questActiveSprite;

    private bool inTrigger = false;

    // Use this for initialization
    protected override void Start () {      
        SetQuestMarker();

        base.Start();
	}

    // Update is called once per frame
    protected override void Update () {

        SetQuestMarker();

        base.Update();
		/*if(intTrigger && Input.GetKeyDown(KeyCode.K))
        {
            //quest ui manager            
            QuestUIManager.uiManagerQ.CheckQuests(this);

            //set NPC to stay put while quest panel is open

        }  */    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            inTrigger = false;
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

    public void TalkToNpC()
    {
        if(inTrigger)
        {
            QuestUIManager.uiManagerQ.CheckQuests(this);
        }
        else
        {
            Debug.Log("Target out of range");
        }
    }
}
