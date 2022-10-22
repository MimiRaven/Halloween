using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PossessObject : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public GameObject scareRadius;
    GameObject player;
    void Start()
    {
        //boxCollider.GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<BoxCollider2D>(), boxCollider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    public void DisableCollider()
    {
        //boxCollider.enabled = false;
    }

    public void EnableCollider()
    {
        //boxCollider.enabled = true;
    }
}
