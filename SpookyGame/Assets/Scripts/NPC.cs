using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public float scareMeter;
    public float successLimit = 10;
    public float failedLimit = 100;

    public NavMeshAgent agent;
    public GameObject point1;
    public GameObject point2;
    public GameObject gameManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.SetDestination(point2.transform.position);
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

    public void IncreaseScare(float x)
    {
        scareMeter += x;
        scareMeter = Mathf.Clamp(scareMeter, 0f, failedLimit);
        
        if (scareMeter == successLimit)
        {
            GameManager g = gameManager.GetComponent<GameManager>();

            Debug.Log("success");
            transform.eulerAngles = Vector3.forward * 90;
            agent.isStopped = true;
            g.Scared();
        }

        if (scareMeter == failedLimit)
        {
            Debug.Log("fail");
        }

        Debug.Log("NPC scare meter: " +scareMeter);
    }
}
