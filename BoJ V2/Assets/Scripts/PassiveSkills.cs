using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSkills : MonoBehaviour
{
    Player player;
    int currPassive1 = 0;
    int currPassive2 = 0;
    int currPassive3 = 0;
    int currPassive4 = 0;
    int currPassive5 = 0;
    int currPassive6 = 0;
    int currPassive7 = 0;
    int currPassive8 = 0;
    int currPassive9 = 0;
    int currPassive10 = 0;

    public GameObject[] passives;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.ourPlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadPassives();
    }

    void LoadPassives()
    {
        passives[0].GetComponentInChildren<Text>().text = "Vigor (" + currPassive1 + "/3)\nGet +10% Max Health";
        passives[5].GetComponentInChildren<Text>().text = "Rage (" + currPassive2 + "/1)\nUnder 40% Health get +20% Damage";
        passives[1].GetComponentInChildren<Text>().text = "Might (" + currPassive3 + "/3)\nGet +5% Damage";
        passives[2].GetComponentInChildren<Text>().text = "Haste (" + currPassive4 + "/3)\nGet +3% Attack Speed";
        passives[3].GetComponentInChildren<Text>().text = "Precise (" + currPassive5 + "/3)\nGet +1% Crit Chance";
        passives[4].GetComponentInChildren<Text>().text = "Brutality (" + currPassive6 + "/3)\nGet +10% Crit Damage";
        passives[6].GetComponentInChildren<Text>().text = "Final Stand (" + currPassive7 + "/1)\nOn fatal damage get +25% Health (2min cd)";
        passives[7].GetComponentInChildren<Text>().text = "Second Wind (" + currPassive8 + "/1)\nUnder 20% Health recover 20% Max Health over 10s (1min cd)";
        passives[8].GetComponentInChildren<Text>().text = "Formidable (" + currPassive9 + "/1)\nFor 3 or more enemy nearby get 4% damage reduction per enemy";
        passives[9].GetComponentInChildren<Text>().text = "Showdown (" + currPassive10 + "/1)\nFor 1 or less enemy nearby get +25% Damage";
    }

    public void Passive1()
    {
        if (player.passivept > 0 && currPassive1 < 3)
        {
            player.AddHP(10);
            player.passivept--;
            currPassive1++;
        }
    }

    public void Passive2()
    {
        if (player.passivept > 0 && currPassive2 < 1)
        {
            player.rage = true;
            player.passivept--;
            currPassive2++;
        }
    }

    public void Passive3()
    {
        if (player.passivept > 0 && currPassive3 < 3)
        {
            player.AddDmg(5);
            player.passivept--;
            currPassive3++;
        }
    }

    public void Passive4()
    {
        if (player.passivept > 0 && currPassive4 < 3)
        {
            player.AddAS(3);
            player.passivept--;
            currPassive4++;
        }
    }

    public void Passive5()
    {
        if (player.passivept > 0 && currPassive5 < 3)
        {
            player.AddCHC(1);
            player.passivept--;
            currPassive5++;
        }
    }

    public void Passive6()
    {
        if (player.passivept > 0 && currPassive6 < 3)
        {
            player.AddCHD(10);
            player.passivept--;
            currPassive6++;
        }
    }

    public void Passive7()
    {
        if (player.passivept > 0 && currPassive7 < 1)
        {
            player.finalstand = true;
            player.passivept--;
            currPassive7++;
        }
    }

    public void Passive8()
    {
        if (player.passivept > 0 && currPassive8 < 1)
        {
            player.secondwind = true;
            player.passivept--;
            currPassive8++;
        }
    }

    public void Passive9()
    {
        if (player.passivept > 0 && currPassive9 < 1)
        {
            player.formidable = true;
            player.passivept--;
            currPassive9++;
        }
    }

    public void Passive10()
    {
        if (player.passivept > 0 && currPassive10 < 1)
        {
            player.showdown = true;
            player.passivept--;
            currPassive10++;
        }
    }
}
