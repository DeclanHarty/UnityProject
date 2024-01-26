using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Horizontal Movement
    [Header("Horizontal Movement Stats")]
    [SerializeField] private float speed = 5;
    private float direction;

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
    private bool canVault;

    private Vector3 lowerVaultPosOffeset = new Vector3(0,.4f, 0);

    // Rigidbody
    private Rigidbody2D rb;

    // Collider
    private CapsuleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate(){
    }

    public void Move(float direction){
        ApplyGravity();

        this.direction = direction;

        if(jumpStored){
            HandleJumpStorage();  
        }

        if(canVault){
            HandleVaulting();
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
        if (!grounded && !canVault) { 
            if(verticalVelocity > TERMINAL_VELOCITY){
                verticalVelocity += gravityAccel * Time.deltaTime;
            }
        }
    }

    private void HandleVaulting(){
        col.isTrigger = true;
    }

    public float GetDireciton(){
        return direction;
    }




}
