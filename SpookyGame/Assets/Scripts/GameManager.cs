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
    public int winTotal = 300;
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

        if (totalScares == endGameTotal && score == winTotal)
        {
            SceneManager.LoadScene("Win Screen");
        }
        
        else if (totalScares == endGameTotal && score < winTotal)
        {
            SceneManager.LoadScene("Lose Screen");
        }
    }

    public void ScareScore(float x)
    {
        score += x;
        scoreText.text = "" + score.ToString();
    }

    public void YouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }
}
