using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Horizontal Movement
    [Header("Horizontal Movement Stats")]
    [SerializeField] private float speed = 5;

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
    [Header("Groundchecking")]
    [SerializeField] private Vector3 groundBoxSize;
    [SerializeField] private float groundCastDistance;
    [SerializeField] private LayerMask groundLayer;
    private bool grounded; 

    // Vault Check Fields
    [Header("Vaultchecking")]
    [Header("Lower Cast")]
    [SerializeField] private Vector3 lowerVaultBoxSize;
    [SerializeField] private float lowerVaultCastDistance;

    private Vector3 lowerVaultPosOffeset = new Vector3(0,.4f, 0);

    // Rigidbody
    private Rigidbody2D rb;



     

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsGrounded();
    }

    void FixedUpdate(){
        IsGrounded();
    }

    public void Move(float direction){
        IsGrounded();
        ApplyGravity();
        

        // if(grounded){
        //     if(Input.GetKeyDown("space") || (jumpStored && timeSinceJumpPressed < jumpStorageLimit)){
        //         verticalVelocity = jumpVelocity;
        //         jumpStored = false;
        //         timeSinceJumpPressed = 0.0f;
        //     }else if(verticalVelocity < 0){
        //         verticalVelocity = 0;
        //     }
            
        // }else {
        //     if(Input.GetKeyDown("space")){
        //         jumpStored = true;
        //         timeSinceJumpPressed = 0;
        //     }
        // }

        if(jumpStored){
            // if(grounded && timeSinceJumpPressed < jumpStorageLimit){
            //     Jump();
            // }else if(timeSinceJumpPressed < jumpStorageLimit){
            //     timeSinceJumpPressed += Time.deltaTime;
                
            // }else{
            //     timeSinceJumpPressed = 0.0f;
            //     jumpStored = false;
            // }
            HandleJumpStorage();  
        }

        rb.position += new Vector2(direction * speed * Time.deltaTime, verticalVelocity * Time.deltaTime);
    }

    public void Jump(){
        if(grounded){
            verticalVelocity = jumpVelocity;
            jumpStored = false;
            timeSinceJumpPressed = 0.0f;
        }else{
            jumpStored = true;
        }
    }

    private void IsGrounded(){
        if(Physics2D.BoxCast(transform.position, groundBoxSize, 0, -transform.up, groundCastDistance, groundLayer)){
            grounded = true;
        }else{
            grounded = false;
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

    private void ApplyGravity(){
        if (verticalVelocity > TERMINAL_VELOCITY && !grounded) { 
            verticalVelocity += gravityAccel * Time.deltaTime;
        }
    }

    private void VaultCheck(){}

    private void HandleVaulting(){
        rb.position += new Vector2(0, 1); 
    }


    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * groundCastDistance, groundBoxSize);
        Gizmos.DrawWireCube(transform.position - lowerVaultPosOffeset + transform.right * lowerVaultCastDistance, lowerVaultBoxSize);
    }


}
