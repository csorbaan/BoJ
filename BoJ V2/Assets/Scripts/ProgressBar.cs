using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float max;
    public float cur;
    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        max = PlayerManager.instance.ourPlayer.GetComponent<Player>().xpcap;
        cur = PlayerManager.instance.ourPlayer.GetComponent<Player>().xp;
        float fillAmout = (float)cur / (float)max;
        mask.fillAmount = fillAmout;
    }
}
