using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler {

    private Bag bag;        // The bag to open or close

    [SerializeField]
    private Sprite full, empty;     // Icons for an equippes slot and empty slot

    public Bag MyBag
    {
        get
        {
            return bag;
        }

        set
        {
            if(value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
    }           //Sets the apropriate icon depending on the bags value

    public void OnPointerClick(PointerEventData eventData)      // Click function for the bag button
    {
        if(bag != null)
        {
            bag.MyBagScript.OpenCloseBag();
        }
    }
    
}
