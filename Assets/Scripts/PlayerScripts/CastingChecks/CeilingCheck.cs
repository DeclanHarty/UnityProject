using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : DrawableBoxCast
{
    [SerializeField] private GameObject player;
    private Movement movement;

    void Awake(){
        movement = player.GetComponent<Movement>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(CastCheck()) movement.HitCeiling();
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
