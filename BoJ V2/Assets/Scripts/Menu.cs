using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void continueGame()
    {
        SceneManager.LoadScene("Teszt");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
