using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWoman2 : NPC
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
        
        // agent.SetDestination(point2.transform.position);
    }


    void Update()
    {
        // if (Mathf.Approximately(transform.position.x, point2.transform.position.x))
        // {
        //     agent.SetDestination(point1.transform.position);
        //     //Debug.Log("new");
        // }

        // if (Mathf.Approximately(transform.position.x, point1.transform.position.x))
        // {
        //     //Debug.Log("complete");
        //     agent.SetDestination(point2.transform.position);
        // }
    }

    private void FixedUpdate()
    {

    }

    // public void IncreaseScare(float x)
    // {
    //     scareMeter += x;
    //     scareMeter = Mathf.Clamp(scareMeter, 0f, failedLimit);
        
    //     if (scareMeter == successLimit)
    //     {
    //         Debug.Log("success");
    //     }

    //     if (scareMeter == failedLimit)
    //     {
    //         Debug.Log("fail");
    //     }

    //     Debug.Log("NPC scare meter: " +scareMeter);
    // }
}
