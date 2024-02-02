using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCheck : DrawableBoxCast
{
    [SerializeField] private GameObject player;
    private Movement movement;

    private bool wallPresent;
    public override bool CastCheck()
    {
        return Physics2D.BoxCast(transform.position + posOffset, boxSize, 0, castDirection, castDistance, castLayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = player.GetComponent<Movement>();
        wallPresent = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool check = CastCheck();
        if(check != wallPresent){
            wallPresent = check;
            movement.UpdateWall(wallPresent, transform.position);
        }
    }

    public Vector2 GetPosition(){
        return transform.position;
    }
}
