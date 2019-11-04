using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private float basedmg = 5;
    private float baseAS = 1;
    private float baseCHC = 5;
    private float baseCHD = 50;
    private float nextpoti;
    private float poticdleft;
    private bool potiused = false;
    private float regentick;
    private float xpcap;

    public Text p_HP;
    public Text poti_CD;
    public Text xp_bar;
    public Text lvl_bar;

    [Header("Stats")]
    public float maxhp;
    public float hp;
    public float dmg;
    public float AS;
    public float STR;
    public float VIT;
    public float DEX;
    public float poticd = 10;
    public int lvl;
    public float hpregen;
    public float CHD;
    public float CHC;
    public float xp;
    public int attribute;

    // Start is called before the first frame update
    void Start()
    {
        lvl = 1;
        STR = 10;
        VIT = 10;
        DEX = 1000;
        xpcap = 500 + lvl * 100;
        MaxHPCalc();
        hp = maxhp;
        poticdleft = poticd;
        p_HP.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Calc();
        p_HP.text = hp.ToString("0");
        lvl_bar.text = lvl.ToString();
        xp_bar.text = xp.ToString("0") + "/" + xpcap.ToString();
        poti_CD.text = poticdleft.ToString("F");
        Poti();
        HpRegen();
        LevelUp();
    }

    public void Hit(float dmg)
    {
        hp -= dmg;
    }

    public void Poti()
    {
        if (poticdleft > 0.0f && potiused)
        {
            poticdleft -= Time.deltaTime;
        }
        else
        {
            potiused = false;
            poticdleft = 0;
        }
        if (Time.time > nextpoti && hp != maxhp)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if ((hp + maxhp * 0.60f) >= maxhp)
                {
                    hp = maxhp;
                }
                else
                {
                    hp += maxhp * 0.60f;
                }
                potiused = true;
                poticdleft = 10;
                nextpoti = Time.time + poticd;
            }
        }
    }

    public void HpRegen()
    {
        if (hp != maxhp)
        {
            if (Time.time > regentick)
            {
                hp += hpregen;
                regentick = Time.time + 1;
            }
        }
    }

    public void GetXP(float exp)
    {
        xp += exp;
    }

    public void LevelUp()
    {
        if (xp >= xpcap)
        {
            attribute += 10;
            xp = xp - xpcap;
            lvl += 1;
            xpcap = 500 + lvl * 100;
        }
    }

    #region Calculation
    public void Calc()
    {
        DmgCalc();
        ASCalc();
        MaxHPCalc();
        HpRegenCalc();
        CHCCalc();
        CHDCalc();
    }

    public void DmgCalc()
    {
        dmg = basedmg + STR;
    }

    public void ASCalc()
    {
        AS = baseAS / (1 + DEX / 100);
    }

    public void MaxHPCalc()
    {
        maxhp = VIT * (10 + lvl / 2f);
    }

    public void HpRegenCalc()
    {
        hpregen = VIT / 100;
    }

    public void CHCCalc()
    {
        CHC = baseCHC + DEX / 100;
    }

    public void CHDCalc()
    {
        CHD = baseCHD + STR / 10;
    }
    #endregion
}
