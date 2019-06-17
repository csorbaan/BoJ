using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class InGameMenü : MonoBehaviour
{
    public GameObject inGameMenu;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            inGameMenu.SetActive(true);
    }

    public void BackToGame()
    {
        inGameMenu.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menü");
    }
}
