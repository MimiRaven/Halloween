using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMain : MonoBehaviour
{
    public GameObject aboutScreen;
    public GameObject creditScreen;
    public GameObject genScreen;

    // Start is called before the first frame update
    void Start()
    {
        aboutScreen.SetActive(false);
        creditScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }


    public void LoadAboutScreen()
    {
        genScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void LoadCreditScreen()
    {
        genScreen.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void ReturntoGenScreen()
    {
        aboutScreen.SetActive(false);
        creditScreen.SetActive(false);
        genScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
