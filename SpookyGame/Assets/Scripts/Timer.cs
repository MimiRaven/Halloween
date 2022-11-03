using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Timer : MonoBehaviour
{
    public bool TimerOn = false;
    public float timeValue = 180;
    public TextMeshProUGUI timeText;

    public GameObject gameManager;
    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager g = gameManager.GetComponent<GameManager>();
        
        DisplayTime(timeValue);
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            TimerEnd();
            timeValue = 0;
            TimerOn = false;
        }
    }

    public void TimerEnd()
    {
        GameObject g = GameObject.FindGameObjectWithTag("GameManager");
        GameManager game = g.GetComponent<GameManager>();

        if (SceneManager.GetActiveScene().name == "Level1Scene")
        {
            if (game.score < game.winTotal)
            {
                SceneManager.LoadScene("Lose Screen");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level2Scene")
        {
            if (game.score < game.winTotal)
            {
                SceneManager.LoadScene("Lose Screen");
            }
        }
    }

    void DisplayTime(float timetoDisplay)
    {
        if (timetoDisplay < 0)
        {
            timetoDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timetoDisplay / 60);
        float seconds = Mathf.FloorToInt(timetoDisplay % 60);
        
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
