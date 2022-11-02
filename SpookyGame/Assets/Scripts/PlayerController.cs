using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using Unity.VisualScripting;
using System;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRen;
    Color blankColor = Color.white;

    public AudioSource playSound;
    private Vector2 movement;
    private Rigidbody2D rb2d;
    public int speed = 5;
    public GameObject possessObject;
    public GameObject possessLight;
    bool possessed;
    bool lightPossessed;
    bool enableMove = true;
    bool possessing;

    private PlayerInput playerInput;
    private GameObject interactiveRef;
    private InputAction InteractAction;
    private InputAction WhisperAction;

    public GameObject Pause;
    private bool paused = false;

    public GameObject theRoom;

    float possessTimer = 2f;
    bool possessCooldown;

    float lightPossessTimer = 2f;
    bool lightPossessCooldown;
    Animator animator;
    float absVelocity;
    Vector2 theVelocity;

    public enum Possess {lightState, objState }
    public Possess possessState;

    private void Awake()
    {
        Time.timeScale = 1;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        InteractAction = playerInput.actions["Interact"];
        WhisperAction = playerInput.actions["Whisper"];
        spriteRen.color = blankColor;

    }

    private void OnMovement(InputValue value) // These are Movement Keys
    {
        if (enableMove == true)
        {
            movement = value.Get<Vector2>();
        }
    }
    void FixedUpdate()
    {
        if (enableMove == true && movement.x != 0 || movement.y != 0)
        {
            rb2d.velocity = movement * speed;
        }
        
        absVelocity = Mathf.Abs(rb2d.velocity.x);
        theVelocity = new Vector2(Mathf.Abs(rb2d.velocity.x), Mathf.Abs(rb2d.velocity.y));

        animator.SetFloat("Move X", rb2d.velocity.x);
        animator.SetFloat("Speed", absVelocity);

        if (possessed == true)
        {
            PossessedObjMovement();
        }
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
        switch(possessState)
        {
            case Possess.lightState:
                if (lightPossessed == true)
                {
                    LightPossessedObjFlickerOn();
                    lightPossessCooldown = true;
                }

                else if (lightPossessed == false && possessLight != null && lightPossessCooldown == false)
                {
                    LightPossessedObjFlickerOff();
                } 
                break;

            case Possess.objState:
                if (possessed == false && possessObject != null && possessCooldown == false)
                {
                    PossessObject p = possessObject.GetComponent<PossessObject>();

                    p.scareRadius.SetActive(true);
                    p.posRen.sortingOrder = 10;
                    blankColor.a = 0;
                    spriteRen.color = blankColor;
                    possessed = true;
                    possessState = Possess.objState;
                }

                else if (possessed == true)
                {
                    PossessedObjAction();
                    possessCooldown = true;
                }
                break;
        }
    }

    private void OnWhisper()
    {
        playSound.Play();
    }

    void Update()
    {
        if (possessCooldown == true)
        {
            possessTimer -= Time.deltaTime;
            if (possessTimer <= 0)
            {
                possessCooldown = false;
                possessTimer = 2f;
            }
        }

        if (lightPossessCooldown == true)
        {
            lightPossessTimer -= Time.deltaTime;
            if (lightPossessTimer <= 0)
            {
                lightPossessCooldown = false;
                lightPossessTimer = 2f;
            }
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CanPossess" && possessing == false)
        {
            PossessObject p = collision.GetComponent<PossessObject>();
            possessObject = collision.gameObject;

            possessState = Possess.objState;

            if (possessCooldown == false)
            {
                p.OnParticles();
            }

            else if (possessCooldown == true)
            {
                p.OffParticles();
            }
        }

        if (collision.tag == "PossessLight" && possessing == false)
        {
            PossessLight l = collision.GetComponent<PossessLight>();
            possessLight = collision.gameObject;

            possessState = Possess.lightState;

            if (lightPossessCooldown == false)
            {
                l.ParticlesOn();
            }

            else if (lightPossessCooldown == true)
            {
                l.ParticlesOff();
            }
        }

        if (collision.tag == "Room")
        {
            theRoom = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("NPC"))
        {
            OnWhisper();
            interactiveRef = collision.gameObject;
            interactiveRef.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CanPossess" && possessing == false)
        {
            PossessObject p = collision.GetComponent<PossessObject>();

            p.OffParticles();

            possessObject = null;
        }

        if (collision.tag == "PossessLight")
        {
            PossessLight l = collision.GetComponent<PossessLight>();

            l.ParticlesOff();
            
            possessLight = null;
        }
        
        if (collision.tag == "Room" && possessing == true)
        {
            transform.position = collision.gameObject.transform.position;
        }

        if (collision.gameObject.CompareTag("NPC"))
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    void PossessedObjMovement()
    {
        Rigidbody2D objRb2d = possessObject.GetComponent<Rigidbody2D>();
        Vector2 position = rb2d.position;
        //Vector2 objPosition = objRb2d.position;

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

        p.posRen.sortingOrder = 2;
        blankColor.a = 1;
        spriteRen.color = blankColor;
        possessing = false;
        possessed = false;
        possessObject = null;
        theRoom = null;
        p.scareRadius.SetActive(false);
    }

    void LightPossessedObjFlickerOff()
    {
        PossessLight l = possessLight.GetComponent<PossessLight>();
        ScareRadius s = l.scareRadius.GetComponent<ScareRadius>();

        speed = 0;

        enableMove = false;
        s.ScareNPC();
        l.FlickerLightOn();

        //possessing = true;
        blankColor.a = 0;
        spriteRen.color = blankColor;
        lightPossessed = true;
    }

    void LightPossessedObjFlickerOn()
    {
        PossessLight l = possessLight.GetComponent<PossessLight>();
        ScareRadius s = l.scareRadius.GetComponent<ScareRadius>();

        speed = 7;

        enableMove = true;
        s.ScareNPC();
        l.FlickerLightOff();

        possessing = false;
        blankColor.a = 1;
        spriteRen.color = blankColor;
        lightPossessed = false;
        possessLight = null;
    }
}
