using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float followDistance;
    private Vector2 position;
    private float travelToPosition;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float verticalVelocityMultipler;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector2(transform.position.x, transform.position.y);
        if(Mathf.Abs(player.GetPosition().y - position.y) > followDistance){
            travelToPosition = player.GetPosition().y;
        }

        if(Mathf.Abs(travelToPosition - transform.position.y) > 0){
            Debug.Log(travelToPosition);
            float verticalVelocity = player.GetVerticalVelocity();

            int direction = travelToPosition - transform.position.y > 0 ? 1 : -1;

            float speedMultiplier = direction == 1 ? Mathf.Max(direction, verticalVelocity * verticalVelocityMultipler) : Mathf.Min(direction, verticalVelocity * verticalVelocityMultipler);

            transform.position += new Vector3(0, speedMultiplier * cameraSpeed * Time.deltaTime, 0);
        }
    }
}
