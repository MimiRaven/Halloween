using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMan : NPC
{
    // public float scareMeter;
    // public float successLimit = 50;
    // public float failedLimit = 100;


    // public GameObject point1;
    // public GameObject point2;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
 
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // GetComponent<NavMeshAgent>().SetDestination(point1.transform.position);
    }

    // public void IncreaseScare(float x)
    // {
    //     scareMeter += x;
    //     scareMeter = Mathf.Clamp(scareMeter, 0f, failedLimit);
        
    //     if (scareMeter == successLimit)
    //     {
    //         Debug.Log("success");
    //         transform.eulerAngles = Vector3.forward * 90;
    //         agent.isStopped = true;
    //     }

    //     if (scareMeter == failedLimit)
    //     {
    //         Debug.Log("fail");
    //     }

    //     Debug.Log("NPC scare meter: " +scareMeter);
    // }
}
