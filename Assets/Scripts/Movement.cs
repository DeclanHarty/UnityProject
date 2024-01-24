using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float gravityAccel = -10;

    [SerializeField] private float verticalVelocity = 0;
    private const float TERMINAL_VELOCITY = -60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Move(){
        float direction = Input.GetAxisRaw("Horizontal");

        if (verticalVelocity > TERMINAL_VELOCITY) { 
            verticalVelocity += gravityAccel * Time.deltaTime;
        }

        transform.position += new Vector3(direction * speed * Time.deltaTime, verticalVelocity * Time.deltaTime, 0);
    }

    public void GroundCheck(){
        
    }


}
