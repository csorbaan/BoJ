using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;
    public GameObject charsheet;
    public GameObject passives;
    bool open, openp;

    private void Start()
    {
        charsheet.SetActive(false);
        passives.SetActive(false);
        open = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            inGameMenu.SetActive(true);

        if (Input.GetKeyDown(KeyCode.I) && open == false)
        {
            charsheet.SetActive(true);
            open = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && open == true)
        {
            charsheet.SetActive(false);
            open = false;
        }

        if (Input.GetKeyDown(KeyCode.P) && openp == false)
        {
            passives.SetActive(true);
            openp = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && openp == true)
        {
            passives.SetActive(false);
            openp = false;
        }
    }

    public void BackToGame()
    {
        inGameMenu.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
