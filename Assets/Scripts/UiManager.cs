﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public Slider hpBar;
    public Text hpText;
    public Text lvlText;
    public PlayerHealthManager playerHealth;

    private PlayerStats ps;
    private static bool uiExists;

	// Use this for initialization
	void Start () {
        if (!uiExists) // if no player exist
        {
            uiExists = true;
            DontDestroyOnLoad(transform.gameObject); // don't destroy player
        }
        else // if player already exists in scene
        {
            Destroy(gameObject); // destroy the duplicated player
        }

        ps = GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update () {
        hpBar.maxValue = playerHealth.playerMaxHealth;
        hpBar.value = playerHealth.playerCurrentHealth;
        hpText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
        lvlText.text = "Lvl: " + ps.currentLevel;
	}
}