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
    }

    public void ScareScore(float x)
    {
        score += x;
        scoreText.text = "" + score.ToString();
        progress.SetValue(score);
        EndGame();
    }

    public void EndGame()
    {
        if (SceneManager.GetActiveScene().name == "Level1Scene")
        {
            if (score >= winTotal)
            {
                SceneManager.LoadScene("Level2Scene");
            }
        }

        else if (SceneManager.GetActiveScene().name == "Level2Scene")
        {
            if (score >= winTotal)
            {
                SceneManager.LoadScene("Win Screen");
            }
        }  
    }

    public void YouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }
}
