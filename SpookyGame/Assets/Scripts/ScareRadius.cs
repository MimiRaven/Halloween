using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareRadius : MonoBehaviour
{
    GameObject npc;
    public float scareValue = 10;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            npc = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            npc = null;
        }
    }

    public void ScareNPC()
    {
        if (npc != null)
        {
            NPC n = npc.GetComponent<NPC>();

            n.IncreaseScare(scareValue);
        }
    }
}
