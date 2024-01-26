using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : DrawableBoxCast
{
    private Movement movement;
    private bool grounded;
    void Awake(){
        movement = GetComponent<Movement>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bool check = CastCheck();
        if(check != grounded){
            grounded = check;
            movement.UpdateGrounded(check);
        }
    }

    public override bool CastCheck()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, castDirection, castDistance, castLayer)){
            return true;
        }else{
            return false;
        }
    }
}
