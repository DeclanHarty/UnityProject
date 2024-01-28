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
    private float input;

    // Gravity Fields
    [Header("Gravity Stats")]
    [SerializeField] private float gravityAccel = -10;

    [SerializeField] private float verticalVelocity = 0;
    private const float TERMINAL_VELOCITY = -60;

    // Jump Fields
    [Header("Jumping Stats")]
    [SerializeField] private float jumpStorageLimit;
    [SerializeField] private float jumpVelocity;
    private bool jumpStored;
    private float timeSinceJumpPressed = 0.0f;

    // Ground Check Fields
    private bool grounded; 

    // Vault Check Fields
    [SerializeField] private bool canVault;
    private bool vaulting;
    [SerializeField] private Vector2 beginClimbOffset;
    [SerializeField] private Vector2 endClimbOffset;
    [SerializeField] private float vaultSpeed;


    // Rigidbody
    private Rigidbody2D rb;

    // Collider
    private CapsuleCollider2D col;

    // LedgeCheck
    private LedgeGrabCheck ledge;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        ledge = GetComponent<LedgeGrabCheck>();
    }

    void FixedUpdate(){
    }

    public void Move(float direction){
        if(canVault){
            HandleVaulting();
            return;
        }else if(vaulting){
            if(Input.GetKeyDown("s")){
                vaulting = false;
            }else if(Input.GetKeyDown("space")){
                Invoke("FinishVault", vaultSpeed);
            }

            return;
        }

        ApplyGravity();

        this.input = direction;
        if(input > 0){
            currentDirection = 1;
        }else if(input < 0){
            currentDirection = -1;
        }

        if(jumpStored){
            HandleJumpStorage();  
        }

        
        rb.position += new Vector2(direction * speed * Time.deltaTime, verticalVelocity * Time.deltaTime);
    }

    public void Jump(){
        if(!canVault && !vaulting){
            if(grounded){
                verticalVelocity = jumpVelocity;
                jumpStored = false;
                timeSinceJumpPressed = 0.0f;
            }else{
                jumpStored = true;
            }
        }
            
    }

    public void UpdateGrounded(bool check){
        grounded = check;
        if(grounded){
            verticalVelocity = 0;
        }
    }

    public void UpdateCanVault(bool check){
        canVault = check;
        Debug.Log(canVault);
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

    public float GetDireciton(){
        return currentDirection;
    }




}
