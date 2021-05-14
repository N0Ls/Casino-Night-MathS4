using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void playGame()
    {
       SceneManager.LoadScene("RouletteGame");
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void quitGame()
    {
        Application.Quit();
    }
}
