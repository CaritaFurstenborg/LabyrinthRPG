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
    private GameObject mainMenu;           //Reference to main menu

    [SerializeField]
    private GameObject keybindMenu;         // ref to keybind menu

    private GameObject[] keybindButtons;

    void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    // Use this for initialization
    void Start () {       

        mainMenu.SetActive(false);
        keybindMenu.SetActive(false);
        healthStat = targetFrame.GetComponentInChildren<Stats>();

        SetUseable(actionButtons[0], SpellBook.MyInstance.GetSpell("Attack"));
        SetUseable(actionButtons[1], SpellBook.MyInstance.GetSpell("Flamethrust"));
        SetUseable(actionButtons[2], SpellBook.MyInstance.GetSpell("Shoot"));

    }
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
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

    public void ToggleMenu()
    {
        if(!mainMenu.activeSelf)        // if menu not active activate
        {
            mainMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(false);      //else deactivate
        }

        Time.timeScale = Time.timeScale > 0 ? 0 : 1; // Pause functio
    }

    public void ShowKeybindMenu()
    {
        keybindMenu.SetActive(true);
        if(mainMenu.activeSelf)
        {
            mainMenu.SetActive(false);
        }
    }

    public void CloseKeybindMenu()
    {
        keybindMenu.SetActive(false);
        if(!mainMenu.activeSelf)
        {
            mainMenu.SetActive(true);
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

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyButton.image.sprite = useable.MyIcon;
        btn.MyButton.image.color = Color.white;
        btn.MyUseable = useable;
    }
}
