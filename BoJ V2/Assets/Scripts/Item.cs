using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string type;
    public string itemName;
    public Sprite icon;
    public bool pickedUp;
    public bool equipped;

    Inventory inv;

    public void ItemEqp ()
    {
        //Weapon
        if (type == "weapon")
        {

        }

        //Armor
        if (type == "armor")
        {
            //inv.EqpArmor();
        }


    }
}
