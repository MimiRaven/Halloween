using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRen;
    public Sprite playerSprite;
    private Vector2 movement;
    private Rigidbody2D rb2d;
    public int speed = 5;
    GameObject possessObject;
    bool possessed;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    private void OnMovement(InputValue value) // These are Movement Keys
    {
        movement = value.Get<Vector2>();
    }
    private void OnPossession() // This IS the Space Key
    {
        if (possessed == false && possessObject != null)
        {
            PossessObject p = possessObject.GetComponent<PossessObject>();
            p.disableCollider();

            spriteRen.sprite = null;
            possessed = true;
        }

        else if (possessed == true)
        {
            PossessedObjAction();
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);

        if (possessed == true)
        {
            PossessedObjMovement();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CanPossess")
        {
            possessObject = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CanPossess")
        {
            possessObject = null;
        }
    }

    void PossessedObjMovement()
    {
        Vector2 position = rb2d.position;
        Rigidbody2D objRb2d = possessObject.GetComponent<Rigidbody2D>();
        Vector2 objPosition = objRb2d.position;
        possessObject.transform.parent = transform;

        // objPosition.x = possessObject.transform.parent.position.x + speed * horizontal * Time.deltaTime;
        // objPosition.y = possessObject.transform.parent.position.x + speed * horizontal * Time.deltaTime;
        objRb2d.MovePosition(position);
    }

    void PossessedObjAction()
    {
        // Can implement functionality like throw object, flicker light, explode, etc... 
        PossessObject p = possessObject.GetComponent<PossessObject>();
        p.enableCollider();

        spriteRen.sprite = playerSprite;
        possessed = false;
        possessObject = null;
    }
}
