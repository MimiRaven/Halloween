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
    public GameObject possessObject;
    GameObject possessLight;
    public bool possessed;
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

    private void OnMovement(InputValue value) // These are Movement Keys
    {
        movement = value.Get<Vector2>();
    }

    private void OnInteract() // This IS the Space and A Button
    {
        if (possessed == false && possessObject != null)
        {
            PossessObject p = possessObject.GetComponent<PossessObject>();
            p.DisableCollider();
        
            spriteRen.sprite = null;
            possessed = true;
        }

        else if (possessed == true)
        {
            PossessedObjAction();
        }


        if (lightPossessed == false && possessLight != null)
        {
            LightPossessedObjFlickerOff();
        }

        else if (lightPossessed == true)
        {
            LightPossessedObjFlickerOn();
        }
    }

    void FixedUpdate()
    {
        if (enableMove == true)
        {
            rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
        }

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
        Rigidbody2D objRb2d = possessObject.GetComponent<Rigidbody2D>();
        Vector2 position = rb2d.position;
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
        p.EnableCollider();

        spriteRen.sprite = playerSprite;
        possessed = false;
        possessObject = null;
    }

    void LightPossessedObjFlickerOff()
    {
        PossessLight l = possessLight.GetComponent<PossessLight>();
        ScareRadius s = l.scareRadius.GetComponent<ScareRadius>();

        enableMove = false;
        s.ScareNPC();
        l.FlickerLightOn();

        
        spriteRen.sprite = null;
        lightPossessed = true;
    }

    void LightPossessedObjFlickerOn()
    {
        PossessLight l = possessLight.GetComponent<PossessLight>();
        ScareRadius s = l.scareRadius.GetComponent<ScareRadius>();

        enableMove = true;
        s.ScareNPC();
        l.FlickerLightOff();

        spriteRen.sprite = playerSprite;
        lightPossessed = false;
        possessLight = null;
    }
}
