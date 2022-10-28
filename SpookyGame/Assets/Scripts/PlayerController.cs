using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRen;
    public Sprite playerSprite;
    public AudioSource playSound;
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

    float possessTimer = 3f;
    bool possessCooldown;

    float lightPossessTimer = 3f;
    bool lightPossessCooldown;

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
        if (possessed == false && possessObject != null && possessCooldown == false)
        {
            PossessObject p = possessObject.GetComponent<PossessObject>();
            p.DisableCollider();
        
            spriteRen.sprite = null;
            possessed = true;
        }

        else if (possessed == true)
        {
            PossessedObjAction();
            possessCooldown = true;
        }


        if (lightPossessed == false && possessLight != null && lightPossessCooldown == false)
        {
            Debug.Log(lightPossessCooldown);
            LightPossessedObjFlickerOff();
        }

        else if (lightPossessed == true)
        {
            LightPossessedObjFlickerOn();
            lightPossessCooldown = true;
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
                possessTimer = 3f;
            }
        }

        if (lightPossessCooldown == true)
        {
            lightPossessTimer -= Time.deltaTime;
            if (lightPossessTimer <= 0)
            {
                lightPossessCooldown = false;
                lightPossessTimer = 3f;
            }
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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "CanPossess" && possessing == false)
        {
            PossessObject p = collision.GetComponent<PossessObject>();
            possessObject = collision.gameObject;

            p.OnParticles();
        }

        if (collision.tag == "PossessLight" && possessing == false)
        {
            PossessLight l = collision.GetComponent<PossessLight>();
            possessLight = collision.gameObject;

            l.ParticlesOn();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            theRoom = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("NPC"))
        {
            OnWhisper();
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
