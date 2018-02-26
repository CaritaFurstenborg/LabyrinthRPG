using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for any items in game, scriptable object so there is no need for a gameobject to sit on
public abstract class Item : ScriptableObject, IMoveable {

    [SerializeField]
    private Sprite icon;                //the image of the item

    [SerializeField]
    private int stackSize;              //sets stacksize of the item

    private InvSlotScript slot;         //The slot of the item in inventory

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public InvSlotScript MySlot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }               // Remove self
}
