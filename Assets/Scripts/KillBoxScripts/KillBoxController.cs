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
    }


    public void Move(){
        rb.position += new Vector2(0, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            GameController.instance.PlayerDies();
        }
    }

}
