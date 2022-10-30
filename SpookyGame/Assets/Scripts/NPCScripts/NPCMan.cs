using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMan : NPC
{
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
}
