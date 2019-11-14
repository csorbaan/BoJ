using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool invEnabled;
    public GameObject inventory;

    private int allSlots;
    private GameObject[] slot;

    public GameObject slotHolder;
    public GameObject armorSlot, weaponSlot;

    // Start is called before the first frame update
    void Start()
    {
        allSlots = 28;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item == null) slot[i].GetComponent<Slot>().empty = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) invEnabled = !invEnabled;
        if (invEnabled == true) inventory.SetActive(true);
        else inventory.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            
            AddItem(itemPickedUp, item.ID, item.type, item.itemName, item.icon, item.str, item.dex, item.vit, item.dmg);
        }
    }

    void AddItem(GameObject itemObject,int itemID, string itemType, string itemName, Sprite itemIcon, int itemStr, int itemDex, int itemVit, int itemDmg)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().itemName = itemName;
                slot[i].GetComponent<Slot>().str = itemStr;
                slot[i].GetComponent<Slot>().dex = itemDex;
                slot[i].GetComponent<Slot>().vit = itemVit;
                slot[i].GetComponent<Slot>().dmg = itemDmg;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;
            }
            else continue;
            return;
        }
    }

    public void ArmorEqp(GameObject itemObject, int itemID, string itemType, string itemName, Sprite itemIcon, int itemStr, int itemDex, int itemVit, int itemDmg)
    {
        armorSlot.GetComponent<EqpSlot>().item = itemObject;
        armorSlot.GetComponent<EqpSlot>().icon = itemIcon;
        armorSlot.GetComponent<EqpSlot>().type = itemType;
        armorSlot.GetComponent<EqpSlot>().ID = itemID;
        armorSlot.GetComponent<EqpSlot>().itemName = itemName;
        armorSlot.GetComponent<EqpSlot>().str = itemStr;
        armorSlot.GetComponent<EqpSlot>().dex = itemDex;
        armorSlot.GetComponent<EqpSlot>().vit = itemVit;
        armorSlot.GetComponent<EqpSlot>().dmg = itemDmg;

        armorSlot.GetComponent<EqpSlot>().eqp = true;
        itemObject.transform.parent = armorSlot.transform;
        armorSlot.GetComponent<EqpSlot>().UpdateSlot();
        armorSlot.GetComponent<EqpSlot>().Equiped();
    }

    public void WeaponEqp(GameObject itemObject, int itemID, string itemType, string itemName, Sprite itemIcon, int itemStr, int itemDex, int itemVit, int itemDmg)
    {
        weaponSlot.GetComponent<EqpSlot>().item = itemObject;
        weaponSlot.GetComponent<EqpSlot>().icon = itemIcon;
        weaponSlot.GetComponent<EqpSlot>().type = itemType;
        weaponSlot.GetComponent<EqpSlot>().ID = itemID;
        weaponSlot.GetComponent<EqpSlot>().itemName = itemName;
        weaponSlot.GetComponent<EqpSlot>().str = itemStr;
        weaponSlot.GetComponent<EqpSlot>().dex = itemDex;
        weaponSlot.GetComponent<EqpSlot>().vit = itemVit;
        weaponSlot.GetComponent<EqpSlot>().dmg = itemDmg;

        weaponSlot.GetComponent<EqpSlot>().eqp = true;
        itemObject.transform.parent = weaponSlot.transform;
        weaponSlot.GetComponent<EqpSlot>().UpdateSlot();
        weaponSlot.GetComponent<EqpSlot>().Equiped();
    }
}
