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
    private Button[] actionButtons;         //action buttons array

    private KeyCode action1, action2, action3;      // action buttons for keybinds

    [SerializeField]
    private GameObject targetFrame;         // reference to the unit frame of target

    private Stats healthStat;               // reference to the health stat of target unityframe

    [SerializeField]
    private Image avatarImage;              // reference to the image of target unitframe

    [SerializeField]
    private GameObject mainMenu;           //Reference to main menu

    // Use this for initialization
    void Start () {
        mainMenu.SetActive(false);
        healthStat = targetFrame.GetComponentInChildren<Stats>();

        //Keybinds
        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;
        
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
    //Method for invoking the on click on button press
    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
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
            Debug.Log("Menu active");
        }
        else
        {
            mainMenu.SetActive(false);      //else deactivate
            Debug.Log("Menu Deactive");
        }

        Time.timeScale = Time.timeScale > 0 ? 0 : 1; // Pause functio
    }

    public void ExitCurrentGame()
    {
        PlayerInfo.playerInfo.Save();
        SceneManager.UnloadSceneAsync("Level1");
        SceneManager.LoadScene("StartScreen");
    }

}
