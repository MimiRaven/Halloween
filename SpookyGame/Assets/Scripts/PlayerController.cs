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
    bool possessed;
    bool lightPossessed;
    bool enableMove = true;
    bool possessing;

    private PlayerInput playerInput;
    private InputAction InteractAction;
    private InputAction WhisperAction;

    public GameObject Pause;
    private bool paused = false;

    public GameObject theRoom;

    private void Awake()
    {
        Time.timeScale = 1;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        InteractAction = playerInput.actions["Interact"];
        WhisperAction = playerInput.actions["Whisper"];
    }

    private void OnMovement(InputValue value) // These are Movement Keys
    {
        movement = value.Get<Vector2>();
    }

    private void OnPauseMenu()
    {
        if (paused == false)
        {
            Pause.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }

        else if (paused == true)
        {
            Pause.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }
    }

    private void OnResume()
    {
        Pause.SetActive(false);
        paused = false;
        Time.timeScale = 1;
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

    private void OnWhisper()
    {

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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "CanPossess" && possessing == false)
        {
            possessObject = collision.gameObject;
        }

        if (collision.tag == "PossessLight" && possessing == false)
        {
            possessLight = collision.gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            theRoom = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CanPossess" && possessing == false)
        {
            possessObject = null;
        }

        if (collision.tag == "PossessLight")
        {
            possessLight = null;
        }
        
        if (collision.tag == "Room" && possessing == true)
        {
            transform.position = collision.gameObject.transform.position;
        }
    }

    void PossessedObjMovement()
    {
        Rigidbody2D objRb2d = possessObject.GetComponent<Rigidbody2D>();
        Vector2 position = rb2d.position;
        Vector2 objPosition = objRb2d.position;

        possessObject.transform.parent = transform;

        possessing = true;
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
        possessing = false;
        possessed = false;
        possessObject = null;
        theRoom = null;
    }

    void LightPossessedObjFlickerOff()
    {
        PossessLight l = possessLight.GetComponent<PossessLight>();
        ScareRadius s = l.scareRadius.GetComponent<ScareRadius>();

        enableMove = false;
        s.ScareNPC();
        l.FlickerLightOn();

        possessing = true;
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

        possessing = false;
        spriteRen.sprite = playerSprite;
        lightPossessed = false;
        possessLight = null;
    }
}
