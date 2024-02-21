using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Horizontal Movement
    [Header("Horizontal Movement Stats")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] private float friction;
    [SerializeField] private int currentDirection = 1;

    // Input
    private Vector2 input;

    // Gravity Fields
    [Header("Gravity Stats")]
    [SerializeField] private float gravityAccel = -10;

    [SerializeField] private float verticalVelocity = 0;
    [SerializeField] private float TERMINAL_VELOCITY = -60;

    // Jump Fields
    [Header("Jumping Stats")]
    [SerializeField] private float jumpStorageLimit;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float earlyReleaseModifier;
    private bool jumpHeld;
    private bool jumpStored;
    private float timeSinceJumpPressed = 0.0f;

    [SerializeField] private float airStrafeMultiplier;

    // Ground Check Fields
    private bool grounded; 
    private float timeSinceLeftGround;
    [SerializeField] private float coyoteTime;

    // Vault Check Fields
    [SerializeField] private bool canVault;
    [SerializeField] private bool vaulting;
    [SerializeField] private bool finishingVault;
    [SerializeField] private Vector2 beginClimbOffset;
    [SerializeField] private Vector2 endClimbOffset;
    [SerializeField] private float vaultSpeed;

    [SerializeField] private float ledgeJumpSpeed;

    // Wall Check Fields
    [SerializeField] private bool wallPresent;
    private Vector2 wallPos;


    // Sliding Fields
    private bool sliding;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;


    // Rigidbody
    private Rigidbody2D rb;

    // Collider
    private CapsuleCollider2D col;

    // LedgeCheck
    [SerializeField] private LedgeGrab ledge;

    // Ground Check
    [SerializeField] private GameObject groundCheckObject;

    // Wall Check
    [SerializeField] private GameObject wallCheckObj;
    // private WallCheck wallCheck;

    // private GroundCheck groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        // groundCheck = groundCheckObject.GetComponent<GroundCheck>();
        // wallCheck = wallCheckObj.GetComponent<WallCheck>();
    }

    void FixedUpdate(){
    }

    public void Move(Vector2 _input, bool _jumpHeld){
        input = _input;

        jumpHeld = _jumpHeld;

        //Checsks grounded if not continues timeSinceLeftGround Timer
        if(!grounded){
            timeSinceLeftGround += Time.deltaTime;
        }

        //Checks vaulting and input and starts the vault process if true
        if(canVault && input.x != 0 && !(verticalVelocity >= 10)){
            HandleVaulting();
            return;
        }else if(vaulting){
            //Stops vaulting if the player presses down
            if(input.y < 0){
                vaulting = false;
            }
            return;
        }

        ApplyGravity();


        // Checks if the player is currently sliding and if they are continues sliding and returns
        if(sliding){
            rb.velocity = new Vector2(slideSpeed * currentDirection, verticalVelocity);
            return;
        }

        
        

        // New Direction is set to zero every frame
        int newDirection = 0;

        //New direction is set by horizontal input
        if(input.x > 0){
            newDirection = 1;
        }else if(input.x < 0){
            newDirection = -1;
        }

        //Current direction is updated only if the direction changes. No input means the direction stays the same
        if(newDirection != currentDirection && newDirection != 0){
            currentDirection = newDirection;
            wallPresent = false;
        }

        //Calculates horizontal velocity
        float multiplier;
        if(grounded){
            multiplier = 1f;
        }else{
            multiplier = airStrafeMultiplier;
        }
        horizontalVelocity += input.x * horizontalAcceleration * multiplier * Time.deltaTime;

        //Adds friction if there is no player movement
        if(input.x == 0){
            float frictionMultiplier;
            if(grounded){
                frictionMultiplier = 1;
            }else{
                frictionMultiplier = .25f;
            }

            horizontalVelocity = Mathf.MoveTowards(horizontalVelocity, 0, friction * frictionMultiplier * Time.deltaTime);
        }
        
        //Clamps player velocity to maxSpeed
        horizontalVelocity = Mathf.Clamp(horizontalVelocity, -maxSpeed, maxSpeed);

        if(jumpStored){
            HandleJumpStorage();  
        }

        //Stops player from moving if a wall is present
        if(wallPresent && !vaulting){
            horizontalVelocity = 0;
        }

        //Updates Players Horizontal Position
        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    public void Jump(){
        if(grounded || timeSinceLeftGround < coyoteTime){
            timeSinceLeftGround += coyoteTime;
            verticalVelocity = jumpVelocity;
            jumpStored = false;
            timeSinceJumpPressed = 0.0f;
        }else if(!sliding && !vaulting && !canVault){
            jumpStored = true;
        }
    }


    private void HandleJumpStorage(){
        if(grounded && timeSinceJumpPressed < jumpStorageLimit){
            Jump();
        }else if(timeSinceJumpPressed < jumpStorageLimit){
            timeSinceJumpPressed += Time.deltaTime;
        }else{
            timeSinceJumpPressed = 0.0f;
            jumpStored = false;
        }
    }

    public void UpdateGrounded(bool check){
        grounded = check;
        if(grounded){
            verticalVelocity = 0;
        }else{
            timeSinceLeftGround = 0;
        }
    }

    public void HitCeiling(){
        verticalVelocity = Mathf.Min(0, verticalVelocity);
    }

    public void UpdateCanVault(bool check){
        canVault = check;
        if(canVault) RemoveVelocity();
    }

    
    private void ApplyGravity(){
        if (!grounded && !vaulting) { 
            if(verticalVelocity > TERMINAL_VELOCITY){
                if(!jumpHeld || verticalVelocity < 0){
                    verticalVelocity += gravityAccel * earlyReleaseModifier * Time.deltaTime;
                    return;
                }
                verticalVelocity += gravityAccel * Time.deltaTime;
            }
        }
    }

    private void HandleVaulting(){
        vaulting = true;
        canVault = false;
        RemoveVelocity();
        horizontalVelocity = 0;
        verticalVelocity = 0;
        rb.position = ledge.getPosition() + new Vector2(beginClimbOffset.x * currentDirection, beginClimbOffset.y);
        wallPresent = false;
        

    }

    private void FinishVault(){
        if(vaulting){
            rb.position = ledge.getPosition() + new Vector2(endClimbOffset.x * currentDirection, endClimbOffset.y);
            vaulting = false;
        }  
    }

    public void HandleSpaceInput(){
        if(!(canVault || vaulting || finishingVault)){
            Jump();
            return;
        }

        if(vaulting){
            if(Mathf.Abs(input.x + currentDirection) >= 1){
                Invoke("FinishVault", vaultSpeed);
                return;
            }else{
                vaulting = false;
                horizontalVelocity = 2 * maxSpeed * -1 *currentDirection;
                verticalVelocity = jumpVelocity;
                return;
            }
            
        }
    }

    // Wall Check
    public void UpdateWall(bool wallPresent, Vector2 wallPos){
        this.wallPresent = wallPresent;
        this.wallPos = wallPos;
    }

    // Slide
    public void Slide(){
        if(!sliding && grounded){
            sliding = true;
            col.size = new Vector2(1, .2f);
            col.offset = new Vector2(0, -.2f);
            Invoke("EndSlide", slideTime);
        }
        
    }

    public void EndSlide(){
        col.size = new Vector2(1, 1);
        col.offset = new Vector2(0, 0);
        sliding = false;
    }

    public void RemoveVelocity(){
        rb.velocity = Vector2.zero;
    }

    public float GetDireciton(){
        return currentDirection;
    }

    public Vector2 GetPosition(){
        return rb.position;
    }

    public float GetVerticalVelocity(){
        return verticalVelocity;
    }

    public bool IsSliding(){
        return sliding;
    }


}
