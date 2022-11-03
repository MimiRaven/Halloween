using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewMain : MonoBehaviour
{
    public GameObject aboutScreen;
    public GameObject creditScreen;
    public GameObject genScreen;
    public GameObject eventSystem;
    EventSystem e;

    public GameObject aboutBack;
    public GameObject creditBack;
    public GameObject playButton;

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
        EventSystem e = eventSystem.GetComponent<EventSystem>();
        genScreen.SetActive(false);
        aboutScreen.SetActive(true);
        e.SetSelectedGameObject(aboutBack);
    }

    public void LoadCreditScreen()
    {
        EventSystem e = eventSystem.GetComponent<EventSystem>();
        genScreen.SetActive(false);
        creditScreen.SetActive(true);
        e.SetSelectedGameObject(creditBack);
    }

    public void ReturntoGenScreen()
    {
        EventSystem e = eventSystem.GetComponent<EventSystem>();
        aboutScreen.SetActive(false);
        creditScreen.SetActive(false);
        genScreen.SetActive(true);
        e.SetSelectedGameObject(playButton);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
