using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour {

    [System.Serializable]
    public class DropItem
    {
        public string name;
        public Item item;
        public int dropRarity;
    }

    [SerializeField]
    private List<DropItem> lootTable = new List<DropItem>();

    [SerializeField]
    private int dropChanse;

	public void CalculateLoot()
    {
        int calc_dopChanse = Random.Range(0, 101);

        if(calc_dopChanse > dropChanse)
        {
            return;
        }

        if(calc_dopChanse <= dropChanse)
        {
            int itemWeight = 0;

            for(int i = 0; i < lootTable.Count; i++)
            {
                itemWeight += lootTable[i].dropRarity;
            }
            Debug.Log("ItemWeight = " + itemWeight);

            int randomValue = Random.Range(0, itemWeight);

            for(int j = 0; j < lootTable.Count; j++)
            {
                Item drop;
                if(randomValue <= lootTable[j].dropRarity)
                {
                    string dropName = lootTable[j].name;
                    if(dropName.Equals("Bag 4 slot"))
                    {
                        Bag bag4s = (Bag)Instantiate(lootTable[j].item);
                        bag4s.Initialize(4);
                        InventoryScript.MyInstance.AddItem(bag4s);
                    }
                    else if(dropName.Equals("Bag 8 slot"))
                    {
                        Bag bag4s = (Bag)Instantiate(lootTable[j].item);
                        bag4s.Initialize(8);
                        InventoryScript.MyInstance.AddItem(bag4s);
                    }
                    else
                    {
                        drop = Instantiate(lootTable[j].item);
                        InventoryScript.MyInstance.AddItem(drop);
                    }
                    return;
                }
                randomValue -= lootTable[j].dropRarity;
            }
        }
    }
}
