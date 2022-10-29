using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class NPC : MonoBehaviour
{
    public float scareMeter;
    public float successLimit = 10;
    public float failedLimit = 100;
    bool canScare = true;
    public NavMeshAgent agent;
    public GameObject point1;
    public GameObject point2;
    GameManager g;
    public SpriteRenderer ren;
    public AudioSource playSound;
    public Color startColor = Color.blue;
    public Color endColor = Color.white;
    public Color nearDeath = new Color (255, 117, 117);

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

    }

    public void IncreaseScare(float x)
    {
        scareMeter += x;

        if (canScare == true)
        {
            GameObject gameManager = GameObject.FindWithTag("GameManager");
            GameManager g = gameManager.GetComponent<GameManager>();
            
            g.ScareScore(x);
            LerpColor();
            playSound.Play();
        }

        if (scareMeter >= failedLimit - 9 && canScare == true)
        {
            ren.color = nearDeath;
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

            g.ScareScore(-scareMeter);
            NPCStates();
            npcState = NPCState.failScared;
        }
    }

    public void NPCStates()
    {
        switch (npcState)
        {
            case NPCState.notScared:
                g.Scared(1);
                break;

            case NPCState.successScared:
                break;

            case NPCState.failScared:
                g.Scared(-1);
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
