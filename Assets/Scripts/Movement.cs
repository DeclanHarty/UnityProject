using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Horizontal Movement
    [SerializeField] private float speed = 5;

    // Gravity Fields
    [SerializeField] private float gravityAccel = -10;

    [SerializeField] private float verticalVelocity = 0;
    private const float TERMINAL_VELOCITY = -60;

    // Jump Fields
    [SerializeField] private float jumpStorageLimit;
    [SerializeField] private float jumpVelocity;
    private bool jumpStored;
    private float timeSinceJumpPressed = 0.0f;

    // Ground Check Fields
    [SerializeField] private Vector3 boxSize;

    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    private bool grounded; 

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
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)){
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


    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }


}
