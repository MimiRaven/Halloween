using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public float scareMeter;
    public float successLimit = 10;
    public float failedLimit = 100;
    bool canScare = true;
    public NavMeshAgent agent;
    public GameObject point1;
    public GameObject point2;
    public GameObject gameManager;
    public SpriteRenderer ren;
    public Color startColor = Color.blue;
    public Color endColor = Color.white;

    public enum NPCState {notScared, successScared, failScared }
    public NPCState npcState;

    void Start()
    {
        // agent = GetComponent<NavMeshAgent>();
        // agent.SetDestination(point2.transform.position);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        ren = GetComponent<SpriteRenderer>();
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
        GameManager g = gameManager.GetComponent<GameManager>();
        scareMeter += x;
        g.ScareScore(x);

        if (canScare == true)
        {
            LerpColor();    
        }

        if (scareMeter == successLimit && canScare == true)
        {
            NPCStates();
            npcState = NPCState.successScared;
        }

        if (scareMeter >= failedLimit && canScare == true)
        {
            agent.isStopped = true;
            transform.eulerAngles = Vector3.forward * 90;
            canScare = false;

            NPCStates();
            npcState = NPCState.failScared;
        }

        Debug.Log("NPC scare meter: " +scareMeter);
    }

    public void NPCStates()
    {
        GameManager g = gameManager.GetComponent<GameManager>();

        switch (npcState)
        {
            case NPCState.notScared:
                g.Scared(1);
                break;

            case NPCState.successScared:
                
                break;

            case NPCState.failScared:
                g.Scared(-1);
                g.ScareScore(-scareMeter);
                break;
            
        }
    }

    public void LerpColor()
    {
        ren.color = startColor;
        Invoke("ReturnColor", 1f);
    }

    public void ReturnColor()
    {
        ren.color = endColor;
    }
}
