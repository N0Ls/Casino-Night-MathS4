using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void playSlotMachine()
    {
       SceneManager.LoadScene("SlotMachine");
    }

    public void goToStats()
    {
        SceneManager.LoadScene("Stats");
    }

    public void goBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
