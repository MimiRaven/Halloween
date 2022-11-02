using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.Rendering.DebugUI;


public class GameManager : MonoBehaviour
{
    public int totalScares;
    public int endGameTotal = 12;
    public float score;
    public int winTotal = 200;
    public TextMeshProUGUI scoreText;
    public Progress progress;

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
        progress.SetValue(score);
    }

    public void EndGame()
    {
        if (SceneManager.GetActiveScene().name == "Level1Scene")
        {
            if (totalScares == endGameTotal && score >= winTotal)
            {
                SceneManager.LoadScene("Level2Scene");
            }

            else if (totalScares == endGameTotal && score < winTotal)
            {
                SceneManager.LoadScene("Lose Screen");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level2Scene")
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

        
    }



    public void YouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }
}
