using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour {

    [SerializeField]
    private GameObject slotPrefab;      //The slot prefab to be instantiated in the bag

    private CanvasGroup canvasGroup;       // Using canvasgroup instead of gameobject so it's always active and reachable

    private List<InvSlotScript> slots = new List<InvSlotScript>();      //List of slots in bag for checking if slot is empty

    public bool IsOpen                  // Check if the bag is open
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    public List<InvSlotScript> MySlots
    {
        get
        {
            return slots;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

	public void AddSlots(int slotCount)     // Sets the bag to the right size
    {
        for(int i = 0; i < slotCount; i++)
        {
            InvSlotScript slot = Instantiate(slotPrefab, transform).GetComponent<InvSlotScript>();
            slots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        foreach(InvSlotScript slot in slots)
        {
            if(slot.IsEmpty)
            {
                slot.AddItem(item);

                return true;
            }
        }

        return false;
    }

    public void OpenCloseBag()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;      //Toggles between shown and invisible
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;     //toggles between block raycast
    }
}
