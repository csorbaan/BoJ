using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour
{
    public float str, dex, vit;
    public float chc, chd, hpregen, attackspeed, hp, dmg, att;
    public Text strtext, dextext, vittext, chctext, chdtext, hpregentext, attackspeedtext, hptext, dmgtext, atttext;
    public Button strbtn, dexbtn, vitbtn;
    public GameObject charsheet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        StatUpload();
        SheetUpdate();
    }

    void StatUpload()
    {
        str = PlayerManager.instance.ourPlayer.GetComponent<Player>().STR;
        dex = PlayerManager.instance.ourPlayer.GetComponent<Player>().DEX;
        vit = PlayerManager.instance.ourPlayer.GetComponent<Player>().VIT;
        chc = PlayerManager.instance.ourPlayer.GetComponent<Player>().CHC;
        chd = PlayerManager.instance.ourPlayer.GetComponent<Player>().CHD;
        hp = PlayerManager.instance.ourPlayer.GetComponent<Player>().maxhp;
        hpregen = PlayerManager.instance.ourPlayer.GetComponent<Player>().hpregen;
        dmg = PlayerManager.instance.ourPlayer.GetComponent<Player>().dmg;
        attackspeed = PlayerManager.instance.ourPlayer.GetComponent<Player>().AS;
        att = PlayerManager.instance.ourPlayer.GetComponent<Player>().attribute;
    }

    void SheetUpdate()
    {
        strtext.text = "Strenght: " + str;
        dextext.text = "Dexterity: " + dex;
        vittext.text = "Vitality: " + vit;
        chctext.text = "Increase critical chanche by: " + chc;
        chdtext.text = "Increase critical damage by: " + chd;
        hptext.text = "Increase health by: " + hp;
        hpregentext.text = "Increase health regenation by: " + hpregen;
        dmgtext.text = "Increase damage by: " + dmg;
        attackspeedtext.text = "Increase attack speed by: " + attackspeed.ToString("0.00");
        atttext.text = "Attributes point left: " + att;
    }

    public void StrInc()
    {
        PlayerManager.instance.ourPlayer.GetComponent<Player>().StrInc();
    }

    public void DexInc()
    {
        PlayerManager.instance.ourPlayer.GetComponent<Player>().DexInc();
    }

    public void VitInc()
    {
        PlayerManager.instance.ourPlayer.GetComponent<Player>().VitInc();
    }
}
