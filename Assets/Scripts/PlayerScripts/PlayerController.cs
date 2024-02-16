using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement movement;

    [SerializeField] private GameObject sprite;

    private Vector2 _input;
    private bool _spaceHeld;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    public void HandleController()
    {
        transform.localScale = new Vector3(movement.GetDireciton(), 1.75f, 0);
        movement.Move(_input, _spaceHeld);
         
    }

    public void CollectInput(){
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _spaceHeld = Input.GetKey("space");

        if(Input.GetButtonDown("Jump")) movement.HandleSpaceInput();
        if(Input.GetKeyDown("left shift")) movement.Slide();

        if(movement.IsSliding()){
            sprite.transform.localScale = new Vector2(1, .5f);
            sprite.transform.localPosition = new Vector2(0,-.25f);
        }else {
            sprite.transform.localScale = new Vector2(1, 1);
            sprite.transform.position = transform.position;
        }  
    }

    public Vector2 GetPosition(){
        return movement.GetPosition();
    }

    public float GetVerticalVelocity(){
        return movement.GetVerticalVelocity();
    }
}
