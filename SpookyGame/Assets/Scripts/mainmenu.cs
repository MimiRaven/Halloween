using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    public void AboutMenu()
    {
        SceneManager.LoadScene("AboutScene");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options Menu");
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("NewMainMenu");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();

    }

   
}