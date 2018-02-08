using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    private static InventoryScript instance;     

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    private List<Bag> bags = new List<Bag>();       //All my bags on bag bar, max 5 bags

    [SerializeField]
    private BagButton[] bagButtons;                 // Clickable bagbuttons

    //DEGUGGING ONLY
    [SerializeField]
    private Item[] items;

    //DEBUGGING ONLY               

    public bool CanAddBag           // Chaeck if there are empty bagslots
    {
        get { return bags.Count < 5; }
    }

    public Item[] Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }           // DEBUGGING ONLY

    private void Awake()        
    {
        
    }

    private void Start()
    {
        if (bagButtons[0].MyBag == null)
        {
            Bag bag = (Bag)Instantiate(items[0]);       // Initialize the first bag on awake, player will always need a starter bag
            bag.Initialize(20);
            bag.Use();
            OpenClose();
        }
    }

    private void Update()
    {
        // DEBUGGING ONLY!!!!!!!!!!!!!!!!!!
        if(Input.GetKeyDown(KeyCode.Alpha9))        
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            bag.Use();
        }
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            AddItem(bag);
        }
        //DEBUGGING ONLY
    }
    
    public void AddBag(Bag bag)     // Adds a bag to an ampty slot
    {
        foreach(BagButton bagButton in bagButtons)
        {
            if(bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        foreach(Bag bag in bags)
        {
            if(bag.MyBagScript.AddItem(item))
            {
                return;
            }
        }
    }

    public void OpenClose()         // Function for all equipped bags toggle
    {
        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen); 
        // if closed bag == true, open all closed bags
        // if closed bag == false, then close all open bags

        foreach(Bag b in bags) {

            if(b.MyBagScript.IsOpen != closedBag)
            {
                b.MyBagScript.OpenCloseBag();
            }
        }
    }
}
