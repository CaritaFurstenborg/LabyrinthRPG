﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public string questItemName;


    // Use this for initialization
    void Start()
    {
        questItemName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.I) && other.gameObject.name == "Player")
        {
            QuestManager.questManager.AddQuestObjective(questItemName, 1);
            gameObject.SetActive(false);
        }
    }
}
