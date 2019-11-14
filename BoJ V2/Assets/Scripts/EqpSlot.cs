using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EqpSlot : MonoBehaviour
{
    public GameObject item;
    public bool empty, eqp;
    public int ID;
    public string type;
    public string itemName;
    public Sprite icon;
    public int str, dex, vit, dmg;

    public void UpdateSlot()
    {
        this.GetComponent<Image>().sprite = icon;
    }

    public void Equiped()
    {
        PlayerManager.instance.ourPlayer.GetComponent<Player>().AddDmg(dmg);
        PlayerManager.instance.ourPlayer.GetComponent<Player>().AddStr(str);
        PlayerManager.instance.ourPlayer.GetComponent<Player>().AddDex(dex);
        PlayerManager.instance.ourPlayer.GetComponent<Player>().AddVit(vit);
    }
}
