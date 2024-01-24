using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float gravityAccel = -10;

    [SerializeField] private float verticalVelocity = 0;
    private const float TERMINAL_VELOCITY = -60;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private Vector3 boxSize;

    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;



     

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(){
        float direction = Input.GetAxisRaw("Horizontal");

        if (verticalVelocity > TERMINAL_VELOCITY && !IsGrounded()) { 
            verticalVelocity += gravityAccel * Time.deltaTime;
        }

        if(IsGrounded()){
            if(Input.GetKeyDown("space")){
                verticalVelocity = jumpVelocity;
            }else if(verticalVelocity < 0){
                verticalVelocity = 0;
            }
            
        }

        rb.position += new Vector2(direction * speed * Time.deltaTime, verticalVelocity * Time.deltaTime);
    }

    private bool IsGrounded(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)){
            return true;
        }else{
            return false;
        }
    }


    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }


}
