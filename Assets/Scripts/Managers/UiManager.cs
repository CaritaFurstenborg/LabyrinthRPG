using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    private static UiManager instance;      // Sets UiManager to singleton

    public static UiManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }

            return instance;
        }
    }       // Property for accessing UiManager instance

    [SerializeField]
    private ActionButton[] actionButtons;         //action buttons array

    [SerializeField]
    private GameObject targetFrame;         // reference to the unit frame of target

    private Stats healthStat;               // reference to the health stat of target unityframe

    [SerializeField]
    private Image avatarImage;              // reference to the image of target unitframe

    [SerializeField]
    private CanvasGroup mainMenu;           //Reference to main menu

    [SerializeField]
    private CanvasGroup keybindMenu;         // ref to keybind menu

    private GameObject[] keybindButtons;        //array to hold keybind buttons

    [SerializeField]
    private CanvasGroup spellBook;

    void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");      // All keybind buttons needs this tag!!!!
    }

    // Use this for initialization
    void Start () {       
        
        healthStat = targetFrame.GetComponentInChildren<Stats>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Escape))       // Esc toggles main menu
        {
            ToggleMenu(mainMenu);
        }
        if(Input.GetKeyDown(KeyCode.P))         // P toggles spellBook
        {
            ToggleMenu(spellBook);
        }
        if(Input.GetKeyDown(KeyCode.B))         // B toggles all equipped bags
        {
            InventoryScript.MyInstance.OpenClose();
        }
    }

    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);
        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        avatarImage.sprite = target.MyAvatar;

        target.healthChanged += new HealthChanged(UpdateTargetFrame);

        target.characterRemoved += new CharacterRemoved(HideTargetFrame);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    //public void ToggleMainMenu()
    //{
    //    if(!mainMenu.activeSelf)        // if menu not active activate
    //    {
    //        mainMenu.SetActive(true);
    //    }
    //    else
    //    {
    //        mainMenu.SetActive(false);      //else deactivate
    //    }

    //    Time.timeScale = Time.timeScale > 0 ? 0 : 1; // Pause functio
    //}

    public void ShowKeybindMenu()
    {
        if (keybindMenu.alpha != 0)
        {
            ToggleMenu(keybindMenu);
        }
        if (mainMenu.alpha == 1)
        {
            ToggleMenu(mainMenu);
        }
    }

    public void CloseKeybindMenu()
    {
        if(keybindMenu.alpha != 1)
        {
            ToggleMenu(keybindMenu);
        }
        if (mainMenu.alpha == 0)
        {
            ToggleMenu(mainMenu);
        }
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void ToggleMenu(CanvasGroup cg)
    {
        cg.alpha = cg.alpha > 0 ? 0 : 1;
        cg.blocksRaycasts = cg.blocksRaycasts == true ? false : true;
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if(clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
        }
    }

    public void ExitToStartScreen()
    {
        GameObject playerScreen = FindObjectOfType<MainGameObjectsManager>().gameObject;
        PlayerInfo.MyInstance.Save();

        Time.timeScale = 1;
        ToggleMenu(mainMenu);

        LevelManagerScript.levelManager.LoadLevel("StartScreen");
        if (playerScreen != null)
        {
            playerScreen.SetActive(false);
        }
    }
}
