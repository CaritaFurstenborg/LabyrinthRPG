using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartStateSelector : MonoBehaviour {

    [SerializeField]
    private GameObject charCreatePanel;

    [SerializeField]
    private GameObject charSelectPanel;

    [SerializeField]
    private GameObject exitCharCreationBtn;

    [SerializeField]
    private GameObject enterCharCreationBtn;


    void Awake () {

        if (File.Exists(Application.persistentDataPath + "/PlayerAccount.sav"))
        {
            ShowSelectionPanel();            
        }
        else
        {
            ShowCreationPanel();
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void ShowSelectionPanel()
    {
        charSelectPanel.SetActive(true);
        charCreatePanel.SetActive(false);
        exitCharCreationBtn.SetActive(false);
        enterCharCreationBtn.SetActive(true);
    }

    public void ShowCreationPanel()
    {        
        charCreatePanel.SetActive(true);
        charSelectPanel.SetActive(false);
        exitCharCreationBtn.SetActive(true);
        enterCharCreationBtn.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
