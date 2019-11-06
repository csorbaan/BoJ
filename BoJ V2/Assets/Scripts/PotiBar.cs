using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotiBar : MonoBehaviour
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
        float fillAmout = (float)cur / (float)max;
        mask.fillAmount = fillAmout;
    }

    public void barUpload(float max, float cur)
    {
        this.max = max;
        this.cur = cur;
    }
}
