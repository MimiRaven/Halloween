using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
 
    public void OptionsMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();

    }

   
}