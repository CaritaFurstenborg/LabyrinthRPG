﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player player;

    private NPC currentTarget;      // any targettable NPC or enemy

    private QuestObject talkTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ClickTarget();
	}

    private void ClickTarget()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if(hit.collider != null)
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = hit.collider.GetComponent<NPC>();

                player.MyTarget = currentTarget.Select();

                UiManager.MyInstance.ShowTargetFrame(currentTarget);
            }
            else // Deselect target
            {
                UiManager.MyInstance.HideTargetFrame();

                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                    currentTarget = null;
                    player.MyTarget = null;                    
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null && hit.collider.GetComponent<QuestObject>() != null)
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                talkTarget = hit.collider.GetComponent<QuestObject>();

                talkTarget.TalkToNpC();
            }
        }
    }
}
