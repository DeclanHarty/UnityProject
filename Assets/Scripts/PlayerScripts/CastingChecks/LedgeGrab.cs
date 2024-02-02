using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject player;
    private Movement movement;
    private bool canVault;

    [SerializeField] private bool groundIntersect;
    private int numberOfColiders;

    private Vector2 ledgePos;

    // Start is called before the first frame update
    void Awake()
    {
        movement = player.GetComponent<Movement>();
        canVault = false;
        Debug.Log((int)groundLayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CastCheck();
    }

    void CastCheck(){
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.right, 0, groundLayer);
        ledgePos = hit.point;
        bool check;
        if (hit){
            check = true;
        }else {
            check = false;
        }
        bool finalCheck = check && !groundIntersect;

        if(finalCheck != canVault){
            canVault = check && !groundIntersect;
            movement.UpdateCanVault(canVault);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer == LayerMask.NameToLayer("Ground")){
            groundIntersect = true;
            numberOfColiders++;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.layer == LayerMask.NameToLayer("Ground")){
            numberOfColiders--;
            if(numberOfColiders == 0){
                groundIntersect = false;
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public Vector2 getPosition(){
        return ledgePos;
    }
}
