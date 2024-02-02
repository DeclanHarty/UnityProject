using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : DrawableBoxCast
{
    [SerializeField] private GameObject player;
    private Movement movement;
    private bool grounded;
    void Awake(){
        movement = player.GetComponent<Movement>();
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
        return Physics2D.BoxCast(transform.position, boxSize, 0, castDirection, castDistance, castLayer);
    }
}
