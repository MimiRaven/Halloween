using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    
    public float scareMeter;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void IncreaseScare(float x)
    {
        scareMeter += x;
        Debug.Log("NPC scare meter: " +scareMeter);
    }
}
