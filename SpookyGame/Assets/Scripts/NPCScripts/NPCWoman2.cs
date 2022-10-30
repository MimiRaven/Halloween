using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWoman2 : NPC
{
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        Nav();
        destCooldown();
    }
}