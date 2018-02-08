using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 1)]
public class HealthPotion : Item, IUseable {

    [SerializeField]
    private int heal;

    public void Use()
    {
        if(Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        {
            Remove();

            Player.MyInstance.MyHealth.MyCurrentValue += heal;
        }        
    }

    
}
