using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{

    public static QuestUIManager uiManagerQ;

    public bool questAvailable = false;
    public bool questRunning = false;
    public bool questPanelActive = false;
    private bool questLogActive = false;
    //Panels
    public GameObject questPanel;
    public GameObject questLogPanel;
    //Quest Objects
    private QuestObject currentQuestObject;
    // quest lists
    public List<Quest> availableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    //buttons
    public GameObject qButton;
    public GameObject qLogButton;
    public List<GameObject> qButtons = new List<GameObject>();
    //private List<GameObject> qLogButtons = new List<GameObject>();

    public GameObject acceptButton;
    public GameObject abandonButton;
    public GameObject completeButton;
    //spacer
    public Transform qButtonSpacer1; // spacer for q buttons
    public Transform qButtonSpacer2; // running q button
    public Transform qLogButtonSpacer; // running in qLog
    // infos
    public Text qTitle;
    public Text qDescription;
    public Text qSummary;
    // Quest log infos
    public Text qLogTitle;
    public Text qLogDescription;
    public Text qLogSummary;
    // grabbed from quest button script
    public QuestButton acceptButtonScript;
    public QuestButton abandonButtonScript;
    public QuestButton completeButtonScript;

    void Start()
    {
        acceptButton = GameObject.Find("CanvasUiManager").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("QPAcceptButton").gameObject;
        acceptButtonScript = acceptButton.GetComponent<QuestButton>();

        abandonButton = GameObject.Find("CanvasUiManager").transform.Find("QuestLogPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("AbandonButton").gameObject;
        abandonButtonScript = abandonButton.GetComponent<QuestButton>();

        completeButton = GameObject.Find("CanvasUiManager").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("QPCompleteButton").gameObject;
        completeButtonScript = completeButton.GetComponent<QuestButton>();

        acceptButton.SetActive(false);
        abandonButton.SetActive(false);
        completeButton.SetActive(false);
    }
    // grabbed from quest button script END...

    //Awake
    void Awake()
    {
        if (uiManagerQ == null)
        {
            uiManagerQ = this;
        }
        else if (uiManagerQ != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        HideQuestPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            questLogActive = !questLogActive;
            //show quest log panel
            ShowQuestLogPanel();

        }
    }

    // called from quest giver
    public void CheckQuests(QuestObject questObj)
    {
        currentQuestObject = questObj;
        QuestManager.questManager.RequestQuest(questObj);
        if ((questRunning || questAvailable) && !questPanelActive)
        {
            ShowQuestPanel();
        }
        else
        {
            Debug.Log("No quests available");
        }
    }

    //show panel
    public void ShowQuestPanel()
    {
        questPanelActive = true;
        questPanel.SetActive(questPanelActive);
        //fill in data
        FillQuestButtons();
    }

    // hide quest panel
    public void HideQuestPanel()
    {
        questPanelActive = false;
        questAvailable = false;
        questRunning = false;
        //clear all text fields
        qTitle.text = "";
        qDescription.text = "";
        qSummary.text = "";
        //clear all lists
        availableQuests.Clear();
        activeQuests.Clear();
        //clear button list
        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        //hide panel
        questPanel.SetActive(questPanelActive);
    }

    // show quest Log
    public void ShowQuestLog(Quest activeQ)
    {
        qLogTitle.text = activeQ.title;
        if (activeQ.progress == Quest.questProgress.ACCEPTED)
        {
            qLogDescription.text = activeQ.hint;
            qLogSummary.text = activeQ.questObjective + ": " + activeQ.questObjectiveCount + "/" + activeQ.questObjectiveRequirement;
        }
        else if (activeQ.progress == Quest.questProgress.COMPLETED)
        {
            qLogDescription.text = activeQ.turnInDescription;
            qLogSummary.text = activeQ.questObjective + ": " + activeQ.questObjectiveCount + "/" + activeQ.questObjectiveRequirement;
        }
    }

    public void ShowQuestLogPanel()
    {
        questLogPanel.SetActive(questLogActive);
        if (questLogActive && !questPanelActive)
        {
            foreach (Quest current in QuestManager.questManager.currentQuests)
            {
                GameObject questButton = Instantiate(qLogButton);
                QLogButton qlButton = questButton.GetComponent<QLogButton>();
                qlButton.questID = current.id;
                qlButton.questTitle.text = current.title;

                questButton.transform.SetParent(qLogButtonSpacer, false);
                qButtons.Add(questButton);
            }
        }
        else if (!questLogActive && !questPanelActive)
        {
            HideQuestLogPanel();
        }
    }

    public void HideQuestLogPanel()
    {
        questLogActive = false;
        qLogTitle.text = "";
        qLogDescription.text = "";
        qLogSummary.text = "";

        //clear button list
        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questLogPanel.SetActive(false);
    }

    //fill buttons for quest panel
    void FillQuestButtons()
    {
        foreach (Quest availableQ in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QuestButton qBScript = questButton.GetComponent<QuestButton>();

            qBScript.questID = availableQ.id;
            qBScript.questTitle.text = availableQ.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
        }

        foreach (Quest activeQ in activeQuests)
        {
            for(int i = 0; i < currentQuestObject.receivableQuestID.Count; i++)
            {
                if(activeQ.id == currentQuestObject.receivableQuestID[i])
                {
                    GameObject questButton = Instantiate(qButton);
                    QuestButton qBScript = questButton.GetComponent<QuestButton>();

                    qBScript.questID = activeQ.id;
                    qBScript.questTitle.text = activeQ.title;

                    questButton.transform.SetParent(qButtonSpacer2, false);
                    qButtons.Add(questButton);
                }
            }
            
        }
    }

    //show quest on button press in questpanel
    public void ShowSelectedQuest(int questID)
    {
        for (int i = 0; i < availableQuests.Count; i++)
        {
            if (availableQuests[i].id == questID)
            {
                qTitle.text = availableQuests[i].title;
                if (availableQuests[i].progress == Quest.questProgress.AVAILABLE)
                {
                    qDescription.text = availableQuests[i].questDescription;
                    qSummary.text = availableQuests[i].questObjective + ": " + availableQuests[i].questObjectiveCount + "/" + availableQuests[i].questObjectiveRequirement;


                }
            }
        }

        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i].id == questID)
            {
                qTitle.text = activeQuests[i].title;
                if (activeQuests[i].progress == Quest.questProgress.ACCEPTED)
                {
                    qDescription.text = activeQuests[i].hint;
                    qSummary.text = activeQuests[i].questObjective + ": " + activeQuests[i].questObjectiveCount + "/" + activeQuests[i].questObjectiveRequirement;
                }
                else if (activeQuests[i].progress == Quest.questProgress.COMPLETED)
                {
                    qDescription.text = activeQuests[i].turnInDescription;
                    qSummary.text = activeQuests[i].questObjective + ": " + activeQuests[i].questObjectiveCount + "/" + activeQuests[i].questObjectiveRequirement;
                }
            }
        }
    }
}
