using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public Text p_HP;

    [Header("Stats")]
    public float hp = 1000;

    // Start is called before the first frame update
    void Start()
    {
        p_HP.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        p_HP.text = hp.ToString();
    }

    public void Hit(float dmg)
    {
        hp -= dmg;
    }
}
