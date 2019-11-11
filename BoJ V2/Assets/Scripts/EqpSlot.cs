using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EqpSlot : MonoBehaviour
{
    public GameObject item;
    public Transform slotIconGO;
    public bool empty;
    public int ID;
    public string type;
    public string itemName;
    public Sprite icon;

    private void Start()
    {
        slotIconGO = transform.GetChild(0);
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }
}
