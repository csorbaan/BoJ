using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public Transform slotIconGO;
    public bool empty;
    public int ID;
    public string type;
    public string itemName;
    public Sprite icon;

    Inventory inv;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
        
    }

    private void Start()
    {
        slotIconGO = transform.GetChild(0);
        inv = PlayerManager.instance.ourPlayer.GetComponent<Inventory>();
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        inv.EqpArmor(item, ID, type,itemName,icon);
    }
}
