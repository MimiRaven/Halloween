using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float scareMeter;
    public float successLimit = 50;
    public float failedLimit = 100;
    
    void Start()
    {
        
    }


    void Update()
    {

    }

    public void IncreaseScare(float x)
    {
        scareMeter += x;
        scareMeter = Mathf.Clamp(scareMeter, 0f, failedLimit);
        
        if (scareMeter == successLimit)
        {
            Debug.Log("success");
        }

        if (scareMeter == failedLimit)
        {
            Debug.Log("fail");
        }

        Debug.Log("NPC scare meter: " +scareMeter);
    }
}
