using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PossessObject : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public GameObject scareRadius;
    GameObject player;
    public SpriteRenderer posRen;
    public GameObject flickerParticles;

    void Start()
    {
        flickerParticles.SetActive(false);
        player = GameObject.FindWithTag("Player");
        posRen = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<BoxCollider2D>(), boxCollider);
    }

    public void OnParticles()
    {
        flickerParticles.SetActive(true);
    }
    public void OffParticles()
    {
        flickerParticles.SetActive(false);
    }
}
