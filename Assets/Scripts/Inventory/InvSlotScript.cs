using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvSlotScript : MonoBehaviour, IPointerClickHandler, IClickable {

    private MacDoubleClickScript macScript;

    private Stack<Item> items = new Stack<Item>();

    [SerializeField]
    private Image icon;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }           // check if the slot is empty

    public Item MyItem
    {
        get
        {
            if(!IsEmpty)
            {
                return items.Peek();
            }
            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return items.Count;
        }
    }

    void Start()
    {
        macScript = FindObjectOfType<MacDoubleClickScript>();
    }

    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;

        return true;
    }

    public void RemoveItem(Item item)
    {
        if(!IsEmpty)
        {
            items.Pop();
            UiManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Left && macScript.DoubleClick)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if(MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
    }
}
