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
    float rageDmg, sdDmg;
    public float addDmg;
    float addCHC;
    float addCHD;
    public float addAS;
    private float addHP;
    private float nextpoti;
    private float poticdleft;
    private float poticdlefti;
    private bool potiused = false;
    private float regentick;
    float fscd, nextfs;
    float swcd, nextsw;
    bool swon;
    float swontick;
    int swoncd;

    public bool rage;
    public bool finalstand;
    public bool secondwind;
    public bool formidable;
    public bool showdown;
    public Text p_HP;
    public Text poti_CD;
    public GameObject potion, deathScreen, playerSpawn;
    public Text xp_bar;
    public Text lvl_bar;
    public int enemyCount;

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
    public float xpcap;
    public int attribute;
    public int passivept;
    public float dmgreduction;

    PotiBar potibar;

    // Start is called before the first frame update
    void Start()
    {
        poticdlefti = 0;
        fscd = 120;
        swcd = 60;
        lvl = 1;
        STR = 10;
        VIT = 10;
        DEX = 10;
        xpcap = 200 + lvl * 250;
        MaxHPCalc();
        hp = maxhp;
        poticdleft = poticd;
        p_HP.text = hp.ToString();
        potibar = potion.GetComponent<PotiBar>();
    }

    // Update is called once per frame
    void Update()
    {
        Calc();
        p_HP.text = hp.ToString("0");
        lvl_bar.text = "lvl: " + lvl.ToString();
        xp_bar.text = xp.ToString("0") + "/" + xpcap.ToString();
        poti_CD.text = poticdleft.ToString("0.0");
        Poti();
        Passives();
        HpRegen();
        LevelUp();
        Death();
    }

    public void Hit(float dmg)
    {
        hp -= (dmg * (1 - dmgreduction / 100));
    }

    public void Poti()
    {
        if (poticdleft > 0.0f && potiused)
        {
            poticdleft -= Time.deltaTime;
            potibar.barUpload(poticd, poticdlefti);
            poticdlefti += Time.deltaTime;
            if (poticdlefti >= 10)
            {
                poticdlefti = 0;
            }
        }
        else
        {
            potiused = false;
            poti_CD.text = "Q";
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
            lvl += 1;
            if (lvl % 3 == 0)
            {
                passivept++;
            }
            attribute += 10;
            xp = xp - xpcap;
            xpcap = 200 + lvl * 250;
        }
    }

    public void Death()
    {
        if (hp<=0)
        {
            deathScreen.SetActive(true);
            PlayerManager.instance.ourPlayer.GetComponent<NavMeshAgent>().enabled = false;
            transform.position = playerSpawn.transform.position;
        }
    }

    public void RespawnBtn()
    {
        deathScreen.SetActive(false);
        hp = maxhp;
        PlayerManager.instance.ourPlayer.GetComponent<NavMeshAgent>().enabled = true;
    }

    #region Passive/StatInc
    void Passives()
    {
        Rage();
        Final_Stand();
        Second_Wind();
        Formidable();
        Showdown();
    }

    public void Rage()
    {
        if (rage && hp <= (maxhp * 0.4))
        {
            rageDmg = 20;
        }
        else
        {
            rageDmg = 0;
        }
    }

    public void Final_Stand()
    {
        if (finalstand && hp <= 0 && Time.time > nextfs)
        {
            hp = (maxhp * 0.4f);
            nextfs = Time.time + fscd;
        }
    }

    public void Second_Wind()
    {
        if (secondwind && hp <= (maxhp * 0.2f) && Time.time > nextsw)
        {
            swon = true;
            nextsw = Time.time + swcd;
        }
        if (swon && Time.time > swontick)
        {
            hp += maxhp * 0.02f;
            swontick = Time.time + 1;
            swoncd++;
            if (swoncd == 10)
            {
                swon = false;
            }
        }
    }

    public void Formidable()
    {
        if (formidable && enemyCount >= 3)
        {
            if (enemyCount < 8)
            {
                dmgreduction = enemyCount * 4;
            }
            else
            {
                dmgreduction = 8 * 4;
            }
        }
    }

    public void Showdown()
    {
        if (showdown)
        {
            if (enemyCount <= 1)
            {
                sdDmg = 25;
            }
            else
            {
                sdDmg = 0;
            }
        }
    }

    public void StrInc()
    {
        if (attribute > 0)
        {
            STR += 1;
            attribute--;
        }
    }

    public void DexInc()
    {
        if (attribute > 0)
        {
            DEX += 1;
            attribute--;
        }
    }

    public void VitInc()
    {
        if (attribute > 0)
        {
            VIT += 1;
            attribute--;
        }
    }

    public void AddHP(float addhp)
    {
        addHP += addhp;
    }

    public void AddDmg(float adddmg)
    {
        addDmg += adddmg;
    }

    public void AddAS(float addas)
    {
        addAS += addas;
    }

    public void AddCHD(float addchd)
    {
        addCHD += addchd;
    }

    public void AddCHC(float addchc)
    {
        addCHC += addchc;
    }
    #endregion

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
        dmg = (basedmg + STR) * (1 + addDmg / 100) * (1 + rageDmg / 100) * (1 + sdDmg / 100);
    }

    public void ASCalc()
    {
        AS = baseAS / (1 + DEX / 100) / (1 + addAS / 100);
    }

    public void MaxHPCalc()
    {
        maxhp = (VIT * (10 + lvl / 2f)) * (1 + addHP / 100);
    }

    public void HpRegenCalc()
    {
        hpregen = VIT / 100;
    }

    public void CHCCalc()
    {
        CHC = baseCHC + DEX / 100 + addCHC;
    }

    public void CHDCalc()
    {
        CHD = baseCHD + STR / 10 + addCHD;
    }
    #endregion
}
