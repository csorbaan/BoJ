using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private float nextpoti;
    private float poticdleft;
    private bool potiused = false;

    public Text p_HP;
    public Text poti_CD;

    [Header("Stats")]
    public float maxhp = 1000;
    public float hp = 0;
    public float poticd = 10;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        poticdleft = poticd;
        p_HP.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        p_HP.text = hp.ToString();
        poti_CD.text = poticdleft.ToString("F");
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
                Poti();
                potiused = true;
                poticdleft = 10;
                nextpoti = Time.time + poticd;
            }
        }
    }

    public void Hit(float dmg)
    {
        hp -= dmg;
    }

    public void Poti()
    {
        if ((hp + maxhp * 0.60f) >= maxhp)
        {
            hp = maxhp;
        }
        else
        {
            hp += maxhp * 0.60f;
        }
    }
}
