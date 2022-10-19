using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int totalScares;
    public int endGameTotal = 12;
    public int score;
    public int winTotal = 300;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scared(int x)
    {
        totalScares += 1;
        score += x;
        scoreText.text = "" + score.ToString();

        if (totalScares == endGameTotal && score == winTotal)
        {
            SceneManager.LoadScene("Win Screen");
        }
        
        else if (totalScares == endGameTotal && score < winTotal)
        {
            SceneManager.LoadScene("Lose Screen");
        }
    }

    public void YouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }
}
