using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EqpSlot : MonoBehaviour
{
    public GameObject item;
    public bool empty;
    public int ID;
    public string type;
    public string itemName;
    public Sprite icon;

    public void UpdateSlot()
    {
        this.GetComponent<Image>().sprite = icon;
    }
}
