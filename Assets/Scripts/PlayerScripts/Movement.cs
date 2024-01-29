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
    [SerializeField] private float speed = 5;
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
    private bool canVault;
    private bool vaulting;
    [SerializeField] private Vector2 beginClimbOffset;
    [SerializeField] private Vector2 endClimbOffset;
    [SerializeField] private float vaultSpeed;

    [SerializeField] private float ledgeJumpSpeed;

    // Sliding Fields
    private bool sliding;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;


    // Rigidbody
    private Rigidbody2D rb;

    // Collider
    private CapsuleCollider2D col;

    // LedgeCheck
    private LedgeGrabCheck ledge;

    // Ground Check
    [SerializeField] private GameObject groundCheckObject;

    private GroundCheck groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        ledge = GetComponent<LedgeGrabCheck>();
        groundCheck = groundCheckObject.GetComponent<GroundCheck>();
    }

    void FixedUpdate(){
    }

    public void Move(Vector2 _input){
        input = _input;

        if(!grounded){
            timeSinceLeftGround += Time.deltaTime;
        }

        if(canVault){
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

        
        if(input.x > 0){
            currentDirection = 1;
        }else if(input.x < 0){
            currentDirection = -1;
        }

        if(jumpStored){
            HandleJumpStorage();  
        }

        
        rb.position += new Vector2(input.x * speed * Time.deltaTime, verticalVelocity * Time.deltaTime);
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
        if (!grounded && !canVault && !vaulting) { 
            if(verticalVelocity > TERMINAL_VELOCITY){
                verticalVelocity += gravityAccel * Time.deltaTime;
            }
        }
    }

    private void HandleVaulting(){
        canVault = false;
        verticalVelocity = 0;
        rb.position = ledge.getLedgePos() + new Vector2(beginClimbOffset.x * ledge.getDirectionScalar(), beginClimbOffset.y);
        vaulting = true;

    }

    private void FinishVault(){
        rb.position = ledge.getLedgePos() + new Vector2(endClimbOffset.x * ledge.getDirectionScalar(), endClimbOffset.y);
        vaulting = false;
    }

    public void HandleSpaceInput(){
        if(!canVault && !vaulting){
            Jump();
            return;
        }

        if(vaulting){
            Debug.Log(Mathf.Abs(input.x + currentDirection));
            if(Mathf.Abs(input.x + currentDirection) >= 1){
                Invoke("FinishVault", vaultSpeed);
                return;
            }else{
                vaulting = false;
                verticalVelocity = jumpVelocity;
                return;
            }
            
        }
    }

    // Slide
    public void Slide(){
        if(!sliding){
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


}
