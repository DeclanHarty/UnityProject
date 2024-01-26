using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabCheck : DrawableBoxCast
{
    [SerializeField] private float clearCheck;
    private bool canVault;
    private Movement movement;
    private int directionScalar = 1;
    public override bool CastCheck()
    {
        if(Physics2D.BoxCast(transform.position + posOffset, boxSize, 0, castDirection, castDistance, castLayer) && !Physics2D.BoxCast(transform.position + posOffset + new Vector3(0,clearCheck,0), boxSize, 0, castDirection * directionScalar, castDistance, castLayer)){
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
