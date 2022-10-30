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
    internal NavMeshAgent agent;
    public GameObject[] navPoints;
    public SpriteRenderer ren;
    public AudioSource playSound;
    public Color startColor = Color.blue;
    public Color endColor = Color.white;
    public Color nearDeath = new Color (255, 117, 117);
    enum NPCState {notScared, successScared, failScared }
    NPCState npcState;
    enum NavState {point0, point1}
    NavState navState;
    float destinationCooldownTimer = 3f;
    bool destinationCooldown;

    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
    }

    public void IncreaseScare(float x)
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        GameManager g = gameManager.GetComponent<GameManager>();

        scareMeter += x;

        if (canScare == true)
        {       
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
        GameObject gameManager = GameObject.FindWithTag("GameManager");
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

    public void Nav()
    {
        if (Mathf.Approximately(transform.position.x, navPoints[0].transform.position.x) && navState == NavState.point0)
        {
            navState = NavState.point1;
            destinationCooldown = true;
        }

        if (Mathf.Approximately(transform.position.x, navPoints[1].transform.position.x) && navState == NavState.point1)
        {
            navState = NavState.point0;
            destinationCooldown = true;
        }

        switch (navState)
        {
            case NavState.point0:
                {
                    if (destinationCooldown == false)
                    {
                        agent.SetDestination(navPoints[0].transform.position);
                    }
                }
                break;

            case NavState.point1:
                {
                    if (destinationCooldown == false)
                    {
                        agent.SetDestination(navPoints[1].transform.position);
                    }
                }
                break;
        }
    }

    public void destCooldown()
    {
        if (destinationCooldown == true)
        {
            destinationCooldownTimer -= Time.deltaTime;
            if (destinationCooldownTimer <= 0)
            {
                destinationCooldown = false;
                destinationCooldownTimer = 3f;
            }
        }
    }
}
