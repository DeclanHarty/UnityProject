using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabCheck : DrawableBoxCast
{
    [SerializeField] private float clearCheck;
    private bool canVault;
    private Movement movement;
    private int directionScalar = 1;

    [SerializeField] private Vector2 ledgePos;


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
        if(Physics2D.BoxCast(transform.position + new Vector3(posOffset.x * directionScalar, posOffset.y, 0), boxSize, 0, castDirection, castDistance * directionScalar, castLayer) && !Physics2D.BoxCast(transform.position + posOffset + new Vector3(0,clearCheck,0), boxSize, 0, castDirection * directionScalar, castDistance, castLayer)){
            ledgePos = transform.position + new Vector3(posOffset.x * directionScalar, posOffset.y, 0);
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

    public Vector2 getLedgePos(){
        return ledgePos;
    }

    public int getDirectionScalar(){
        return directionScalar;
    }
}
