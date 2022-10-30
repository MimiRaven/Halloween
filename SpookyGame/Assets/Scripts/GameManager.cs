using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int totalScares;
    public int endGameTotal = 12;
    public float score;
    public int winTotal = 200;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Scared(int x) 
    {
        totalScares += x;
        EndGame();
    }

    public void ScareScore(float x)
    {
        score += x;
        scoreText.text = "" + score.ToString();
    }

    public void EndGame()
    {
        if (totalScares == endGameTotal && score >= winTotal)
        {
            SceneManager.LoadScene("Win Screen");
        }

        else if (totalScares == endGameTotal && score < winTotal)
        {
            SceneManager.LoadScene("Lose Screen");
        }
    }

    public void TimerEnd()
    {
        if (score >= winTotal)
        {
            SceneManager.LoadScene("Win Screen");
        }

        if (score < winTotal)
        {
            SceneManager.LoadScene("Lose Screen");
        }
    }

    public void YouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }
}
