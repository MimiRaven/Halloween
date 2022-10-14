using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRen;
    public Sprite playerSprite;
    Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    float speed = 5;
    GameObject possessObject;
    bool possessed;

    void Update()
    {
        Vector2 position = rb2d.position;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.Space))
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
    }


    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rb2d.MovePosition(position);

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

        objPosition.x = possessObject.transform.parent.position.x + speed * horizontal * Time.deltaTime;
        objPosition.y = possessObject.transform.parent.position.x + speed * horizontal * Time.deltaTime;
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