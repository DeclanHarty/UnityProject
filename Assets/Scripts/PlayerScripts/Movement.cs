using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Horizontal Movement
    [Header("Horizontal Movement Stats")]
    [SerializeField] private float maxSpeed;
    private float horizontalVelocity;
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
    private bool jumpStored;
    private float timeSinceJumpPressed = 0.0f;

    // Ground Check Fields
    private bool grounded; 
    private float timeSinceLeftGround;
    [SerializeField] private float coyoteTime;

    // Vault Check Fields
    [SerializeField] private bool canVault;
    [SerializeField] private bool vaulting;
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
    private WallCheck wallCheck;

    private GroundCheck groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        groundCheck = groundCheckObject.GetComponent<GroundCheck>();
        wallCheck = wallCheckObj.GetComponent<WallCheck>();
    }

    void FixedUpdate(){
    }

    public void Move(Vector2 _input){
        input = _input;

        if(!grounded){
            timeSinceLeftGround += Time.deltaTime;
        }

        if(canVault && input.x != 0){
            HandleVaulting();
            return;
        }else if(vaulting){
            if(input.y < 0){
                vaulting = false;
            }
            return;
        }

        if(sliding){
            rb.position += new Vector2(slideSpeed * currentDirection * Time.deltaTime, 0);
            return;
        }

        
        ApplyGravity();

        
        int newDirection = 0;

        if(input.x > 0){
            newDirection = 1;
        }else if(input.x < 0){
            newDirection = -1;
        }

        if(newDirection != currentDirection && newDirection != 0){
            currentDirection = newDirection;
            wallPresent = false;
        }

        horizontalVelocity += input.x * horizontalAcceleration * Time.deltaTime;

        if(input.x == 0){
            horizontalVelocity = Mathf.MoveTowards(horizontalVelocity, 0, friction * Time.deltaTime);
        }
        

        horizontalVelocity = Mathf.Clamp(horizontalVelocity, -maxSpeed, maxSpeed);

        if(jumpStored){
            HandleJumpStorage();  
        }

        rb.position += new Vector2(horizontalVelocity * Time.deltaTime, 0);

        if(wallPresent && !vaulting){
            horizontalVelocity = 0;
        }

        rb.position += new Vector2(0, verticalVelocity * Time.deltaTime);
    }

    public void Jump(){
        if(grounded || timeSinceLeftGround < coyoteTime){
            timeSinceLeftGround += coyoteTime;
            verticalVelocity = jumpVelocity;
            jumpStored = false;
            timeSinceJumpPressed = 0.0f;
        }else{
            jumpStored = true;
        }
    }

    public void EndJumpEarly(){
        if(!grounded && verticalVelocity > 0){
            verticalVelocity = 0;
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
    }

    
    private void ApplyGravity(){
        if (!grounded && !vaulting) { 
            if(verticalVelocity > TERMINAL_VELOCITY){
                verticalVelocity += gravityAccel * Time.deltaTime;
            }
        }
    }

    private void HandleVaulting(){
        canVault = false;
        verticalVelocity = 0;
        horizontalVelocity = 0;
        rb.position = ledge.getPosition() + new Vector2(beginClimbOffset.x, beginClimbOffset.y);
        wallPresent = false;
        vaulting = true;

    }

    private void FinishVault(){

        rb.position = ledge.getPosition() + new Vector2(endClimbOffset.x * currentDirection, endClimbOffset.y);
        vaulting = false;
    }

    public void HandleSpaceInput(){
        if(!canVault && !vaulting){
            Jump();
            return;
        }

        if(vaulting){
            if(Mathf.Abs(input.x + currentDirection) >= 1){
                Invoke("FinishVault", vaultSpeed);
                return;
            }else{
                vaulting = false;
                horizontalVelocity = maxSpeed * currentDirection;
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
