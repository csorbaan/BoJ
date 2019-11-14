using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject image;

    public void continueGame()
    {
        SceneManager.LoadScene("Teszt");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void help()
    {
        image.SetActive(true);
    }

    public void back()
    {
        image.SetActive(false);
    }
}
