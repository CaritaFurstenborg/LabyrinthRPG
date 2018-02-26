using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvSlotScript : MonoBehaviour, IPointerClickHandler, IClickable {

    private MacDoubleClickScript macScript;

    private ObservableStack<Item> items = new ObservableStack<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }           // check if the slot is empty

    public bool IsFull
    {
        get
        {
            if(IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }

            return true;
        }
    }

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

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
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

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if(IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if(IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }

            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        if(!IsEmpty)
        {
            items.Pop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(InventoryScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                InventoryScript.MyInstance.FromSlot = this;
            }
            else if(InventoryScript.MyInstance.FromSlot != null) // if we have something to move
            {
                if(PutItemBack() || AddItems(InventoryScript.MyInstance.FromSlot.items))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
        }
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

    public bool StackItem(Item item)
    {
        if(!IsEmpty && item.name == MyItem.name && items.Count < item.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }

    private bool PutItemBack()
    {
        if(InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }

        return false;
    }

    private void UpdateSlot()
    {
        UiManager.MyInstance.UpdateStackSize(this);
    }
}
