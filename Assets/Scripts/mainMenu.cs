using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    // Index 0 - Main Menu    Index 1 - Controls Menu     Index 2 - Game      Index 3 - Credits
    public void OpenCreditsMenu()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenControlMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
