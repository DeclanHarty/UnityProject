using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float maxDistanceFromPlayer;

    private NewGameController gameController;

    public void Awake(){
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public void SetGameController(NewGameController gameController){
        this.gameController = gameController;
    }


    public void StartMoving(){
        rb.velocity = new Vector2(0, speed);
    }

    public void StopMoving(){
        rb.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
           gameController?.PlayerDies();
        }
    }

    public Vector2 GetPosition(){
        return rb.position;
    }
    public void SetPosition(Vector2 newPos){
        rb.position = newPos;
    }

    public float GetMaxDistanceFromPlayer(){
        return maxDistanceFromPlayer;
    }

}
