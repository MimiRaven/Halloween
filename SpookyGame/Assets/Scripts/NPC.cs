using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class NPC : MonoBehaviour
{
    float scareMeter;
    public float successLimit = 10;
    public float failedLimit = 100;
    bool canScare = true;
    internal NavMeshAgent agent;
    public GameObject[] navPoints;
    public bool isMoving;
    SpriteRenderer ren;
    public AudioSource playSound;
    public Color startColor = new(255, 153, 153);
    public Color endColor = Color.white;

    enum NPCState {notScared, successScared, failScared }
    NPCState npcState;
    enum NavState {point0, point1, dead}
    NavState navState;
    float destinationCooldownTimer = 3f;
    bool destinationCooldown;
    //internal Animator animator;
    public int direction = -1;

    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void FixedUpdate()
    {
        if (isMoving == true)
        {
            Nav();
            destCooldown();
        }
    }

    public void IncreaseScare(float x)
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        GameManager g = gameManager.GetComponent<GameManager>();

        scareMeter += x;
        
        if (canScare == true)
        {
            //animator.SetTrigger("Scared");
            g.ScareScore(x);
            playSound.Play();
        }

        if (scareMeter >= failedLimit - 9 && canScare == true)
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
            transform.eulerAngles = Vector3.forward * 90;
            agent.isStopped = true;
            canScare = false;
            //animator.SetTrigger("Death");
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
                navState = NavState.dead;         
                break;
        }
    }

    public void Nav()
    {
        if (Mathf.Approximately(transform.position.x, navPoints[0].transform.position.x) && navState == NavState.point0)
        {
            navState = NavState.point1;
            destinationCooldown = true;
            //animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        if (Mathf.Approximately(transform.position.x, navPoints[1].transform.position.x) && navState == NavState.point1)
        {
            navState = NavState.point0;
            destinationCooldown = true;
            //animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        switch (navState)
        {
            case NavState.point0:
                {
                    if (destinationCooldown == false)
                    {
                        agent.SetDestination(navPoints[0].transform.position);
                        //animator.SetFloat("Move X", direction);
                        //animator.SetFloat("Speed", agent.velocity.magnitude);
                    }
                }
                break;

            case NavState.point1:
                {
                    if (destinationCooldown == false)
                    {
                        agent.SetDestination(navPoints[1].transform.position);
                        //animator.SetFloat("Move X", -direction);
                        //animator.SetFloat("Speed", agent.velocity.magnitude);
                    }
                }
                break;

            case NavState.dead:
                {
                    isMoving = false;
                    //animator.SetTrigger("Death");
                    //animator.SetFloat("Speed", agent.velocity.magnitude);                 
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
