using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        GameManager g = gameManager.GetComponent<GameManager>();
        
        DisplayTime(timeValue);
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            TimerOn = false;
            g.YouLose();
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
