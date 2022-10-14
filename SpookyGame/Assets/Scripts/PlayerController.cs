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
    GameObject possessLight;
    bool possessed;
    bool lightPossessed;
    bool enableMove = true;

    private PlayerInput playerInput;
    private InputAction InteractAction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        InteractAction = playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        InteractAction.performed +=_=> possessedStart();
        InteractAction.canceled +=_=> possessedStop();
    }

    private void OnDisable()
    {
        InteractAction.performed -=_=> possessedStart();
        InteractAction.canceled -=_=> possessedStop();
    }

    public void possessedStart()
    {
        possessed = true;
        spriteRen.sprite = null;
        PossessObject p = possessObject.GetComponent<PossessObject>();
        p.disableCollider();
        PossessedObjMovement();

    }

    public void possessedStop()
    {
        possessed = false;
        PossessedObjAction();
    }

    private void OnMovement(InputValue value) // These are Movement Keys
    {
        if (enableMove == true)
        {
            movement = value.Get<Vector2>();
        }
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


        if (lightPossessed == false && possessLight != null)
        {
            PossessLight l = possessLight.GetComponent<PossessLight>();
            ScareRadius s = l.scareRadius.GetComponent<ScareRadius>();

            s.ScareNPC();
            l.FlickerLightOn();

            enableMove = false;
            spriteRen.sprite = null;
            lightPossessed = true;
        }

        else if (lightPossessed == true)
        {
            PossessLight l = possessLight.GetComponent<PossessLight>();
            ScareRadius s = l.scareRadius.GetComponent<ScareRadius>();

            s.ScareNPC();
            l.FlickerLightOff();

            enableMove = true;
            spriteRen.sprite = playerSprite;
            lightPossessed = false;
            possessLight = null;
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

                if (collision.tag == "PossessLight")
                {
                    possessLight = collision.gameObject;
                }
            }

            void OnTriggerExit2D(Collider2D collision)
            {
                if (collision.tag == "CanPossess")
                {
                    possessObject = null;
                }

                if (collision.tag == "PossessLight")
                {
                    possessLight = null;
                }
            }

            void PossessedObjMovement()
            {
                Vector2 position = rb2d.position;
                Rigidbody2D objRb2d = possessObject.GetComponent<Rigidbody2D>();
                Vector2 objPosition = objRb2d.position;
                possessObject.transform.parent = transform;

                objRb2d.MovePosition(position);
            }

            void PossessedObjAction()
            {
                // Can implement functionality like throw object, flicker light, explode, etc... 
                PossessObject p = possessObject.GetComponent<PossessObject>();
                ScareRadius s = p.scareRadius.GetComponent<ScareRadius>();

                s.ScareNPC();
                p.enableCollider();

                spriteRen.sprite = playerSprite;
                possessed = false;
                possessObject = null;
            }
        }
