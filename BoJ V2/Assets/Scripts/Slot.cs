using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public bool empty;
    public int ID;
    public string type;
    public string itemName;
    public Sprite icon;
    Inventory inv;

    void Start()
    {
        inv = PlayerManager.instance.ourPlayer.GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }

    public void UpdateSlot()
    {
        this.GetComponent<Image>().sprite = icon;
    }

    public void UseItem()
    {
        inv.ArmorEqp(item, ID, type, itemName, icon);
    }
}
