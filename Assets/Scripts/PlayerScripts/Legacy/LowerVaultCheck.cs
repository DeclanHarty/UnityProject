using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerVaultCheck : DrawableBoxCast
{
    private bool canVault;
    private Movement movement;
    private int directionScalar = 1;
    public override bool CastCheck()
    {
        float inputDirection = movement.GetDireciton();
        if(inputDirection > 0){
            directionScalar = 1;
        }else if(inputDirection < 0){
            directionScalar = -1;
        }else{
            directionScalar = 0;
        }
        if(Physics2D.BoxCast(transform.position + posOffset, boxSize, 0, castDirection * directionScalar, castDistance, castLayer)){
            return true;
        }else{
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        bool check = CastCheck();
        if(check != canVault){
            canVault = check;
            movement.UpdateCanVault(check);
        }
    }
}
