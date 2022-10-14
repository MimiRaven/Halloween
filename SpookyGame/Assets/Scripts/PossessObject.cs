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
    public void disableCollider()
    {
        boxCollider.isTrigger = true;
    }

    public void enableCollider()
    {
        boxCollider.isTrigger = false;
    }
}
