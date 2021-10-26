using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float defaultJump;
    [SerializeField] private GameObject fillBarObj;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private GameObject oliDummy;
    [SerializeField] private int teleportMax;

    public Rigidbody2D rigidBody;
    public LayerMask layermask;
    public Transform groundCollider;


    private float _horizontalDirection, _verticalDirection;
    private float currentTime;
    private float oldPosition;
    private float currentDistance;
    private float showAlpha;
    private float hideAlpha = .75f;
    private bool isOnGround;
    private bool aboutToJump;
    private bool isTeleportAvailable;
    private Vector2 movement;
    private Vector2 direction;
    private Vector2 jumpVelocity;
    private int currentTeleportValue = 0;


    private FillScript scaredFill;
    private Animator playerAnimator;
    private AudioManager audioManager;
    private SpriteRenderer sr;
    private bool isTeleporting;

    private void Awake()
    {
        scaredFill = fillBarObj.GetComponent<FillScript>();
        rigidBody = transform.GetComponent<Rigidbody2D>();
        playerAnimator = transform.GetComponent<Animator>();
    }
    void Start()
    {

        if (!PlayerPrefs.HasKey("unlock"))
        {
            PlayerPrefs.SetString("unlock", "false");
        }

        sr = gameObject.GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();

        currentDistance = transform.position.y;
        oldPosition = currentDistance;


    }

    IEnumerator showOli()
    {
        while (sr.color.a < 1)
        {
            showAlpha += .25f;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b,showAlpha);
           
            yield return new WaitForSeconds(.1f);
        }
        isTeleporting = false;
        showAlpha = 0;
    }

    IEnumerator hideDummy()
    {
        GameObject dummy = Instantiate(oliDummy, transform.position, transform.rotation);
        SpriteRenderer srhide;
        srhide = dummy.GetComponent<SpriteRenderer>();

        while(srhide.color.a > 0)
        {
            hideAlpha -= .25f;
            srhide.color = new Color(sr.color.r, sr.color.g, sr.color.b, hideAlpha);
            yield return new WaitForSeconds(.25f);
        }
        hideAlpha = .75f;
        Destroy(dummy);
    }

    public bool canTeleport()
    {
        return isTeleportAvailable;
    }

    void Update()
    {

            if (Input.GetMouseButtonDown(2) && isTeleportAvailable)
            {
                Camera cam = FindObjectOfType<Camera>();
               
                if (cam != null)
                {

                    audioManager.queueSound("teleport");
                    Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 changePlayerPos = new Vector2(mousePos.x, mousePos.y);
                    currentTeleportValue = 0;
                    playerAnimator.SetBool("isTeleport", false);
                    isTeleportAvailable = false;
                    Color color = sr.color;
                    color.a = 0;
                    sr.color = color;

                    isTeleporting = true;
                    StartCoroutine(hideDummy());
                    StartCoroutine(showOli());
                    transform.position = changePlayerPos;

                }
            }


       _horizontalDirection = Input.GetAxisRaw("Horizontal");
       _verticalDirection = Input.GetAxisRaw("Vertical");

        movement.x = _horizontalDirection;
        movement.y = _verticalDirection;

        isOnGround = Physics2D.OverlapCircle(groundCollider.position, .2f, layermask);

        jumpVelocity = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.F))
        {
            scaredFill.setSliderValue(scaredFill.getSliderValue() + 10);
        }
        
        if(scaredFill.getSliderValue() < 25)
        {
            maxJumpTime = 1.0f;
        }

        if(scaredFill.getSliderValue() >= 25)
        {
            maxJumpTime = 1.2f;
        }

        if(scaredFill.getSliderValue() >= 50.0f)
        {
            maxJumpTime = 1.4f;
        }

        if(scaredFill.getSliderValue() >= 100.0f)
        {
            maxJumpTime = 1.6f;
        }

        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && isOnGround)
            {
                currentTime = Time.time;
                aboutToJump = true;
            }

            //  maxJump = 2.0 
            if (Input.GetMouseButtonUp(0) && isOnGround && aboutToJump)
            {
                audioManager.queueSound("jump");

                direction = new Vector2(-1.1f, 2.0f);

                float jumpMultiplier = Mathf.Max(defaultJump * maxJumpHeight, ((Mathf.Min(Mathf.Abs(currentTime - Time.time), maxJumpTime) / maxJumpTime) * maxJumpHeight));
                jumpVelocity = direction * speed * jumpMultiplier * Time.deltaTime /*((Mathf.Min(Mathf.Max(Mathf.Abs(currentTime - Time.time),defaultJump),maxJumpTime) / maxJumpTime) * maxJumpHeight)*/ /** Mathf.Min(maxJump, Mathf.Max(defaultJump, Mathf.Abs(currentTime - Time.time)))*/;
                rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
                currentTime = 0;
                aboutToJump = false;
            }

            if (Input.GetMouseButtonDown(1) && isOnGround)
            {
                currentTime = Time.time;
                aboutToJump = true;
            }

            if (Input.GetMouseButtonUp(1) && isOnGround && aboutToJump)
            {

                audioManager.queueSound("jump");
                direction = new Vector2(1.1f, 2.0f);
                float jumpMultiplier = Mathf.Max(defaultJump * maxJumpHeight, ((Mathf.Min(Mathf.Abs(currentTime - Time.time), maxJumpTime) / maxJumpTime) * maxJumpHeight));

                jumpVelocity = direction * speed * jumpMultiplier * Time.deltaTime  /*((Mathf.Min(Mathf.Max(Mathf.Abs(currentTime - Time.time),defaultJump),maxJumpTime) / maxJumpTime) * maxJumpHeight)*/ /** Mathf.Min(maxJump, Mathf.Max(defaultJump, Mathf.Abs(currentTime - Time.time)))*/;
                rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
                currentTime = 0;
                aboutToJump = false;
            }

        }

        if (isTeleporting)
        {
            rigidBody.velocity = Vector2.zero;
        }
        else { 
          rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
        }
        
        //SCARED BAR
        if (isOnGround && (currentDistance < transform.position.y || currentDistance > transform.position.y))
        {
            float oldDistance = currentDistance - oldPosition;
                currentDistance = transform.position.y;

            if (Mathf.Round(currentDistance - oldPosition) >= 1 || Mathf.Round(currentDistance - oldPosition) <= -1)
            {
                //       Debug.Log(Mathf.Round(currentDistance - oldPosition));
                int distance = (int)Mathf.Round(currentDistance - oldPosition);
                int difference = scaredFill.getSliderValue() - (distance * 5);
                scaredFill.setSliderValue(difference);

                if(Mathf.Sign(distance) == -1)
                {
                 //   Debug.Log("")
                    currentTeleportValue += Mathf.Abs(distance);
                  
                    if (currentTeleportValue >= teleportMax)
                    {
                        isTeleportAvailable = true;
                        playerAnimator.SetBool("isTeleport", true);
                    }
                }
                
            }
               
                oldPosition = currentDistance;

        }

      //  Debug.Log(currentTeleportValue);

      
    }

    private void OnDisable()
    {
       
    }



    private void FixedUpdate()
    {
       
    }
}
