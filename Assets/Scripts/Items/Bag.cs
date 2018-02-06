using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Createable from assets
[CreateAssetMenu(fileName = "Bag", menuName ="Items/Bag", order =1)]
public class Bag : Item, IUseable {

    private int slots;          //Number of slots in bag

    [SerializeField]
    private GameObject bagPrefab;       

    public BagScript MyBagScript { get; set; }

    public int MySlots
    {
        get
        {
            return slots;
        }
    }

    public void Initialize(int slots)
    {
        this.slots = slots;
    }
    
    public void Use()          //This will equip the bag
    {
        if(InventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slots);

            InventoryScript.MyInstance.AddBag(this);
        }        
    }
}
