using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PossessObject : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public GameObject scareRadius;

    void Start()
    {
        boxCollider.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    public void DisableCollider()
    {
        boxCollider.enabled = false;
    }

    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }
}
