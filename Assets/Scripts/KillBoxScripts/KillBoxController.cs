using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rigidbody2D rb;
    
    [SerializeField] private float speed;

    public void Awake(){
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }


    public void StartMoving(){
        rb.velocity = new Vector2(0, speed);
    }

    public void StopMoving(){
        rb.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            NewGameController.instance.PlayerDies();
        }
    }

}
