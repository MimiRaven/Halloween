using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalScares;
    public int winTotal = 12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scared()
    {
        totalScares += 1;

        if (totalScares == winTotal)
        {
            SceneManager.LoadScene("Win Screen");
        }
    }
}
