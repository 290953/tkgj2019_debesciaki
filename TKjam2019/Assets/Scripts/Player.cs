using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private PlayerModel playerModel;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        playerModel.isMoving = horizontal != 0 || vertical != 0;
//        playerModel._rigidbody.velocity = new Vector3(horizontal * speed, playerModel._rigidbody.velocity.y, vertical * speed);
playerModel.transform.position = new Vector3(transform.position.x, playerModel.transform.position.y, transform.position.z);
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
        
        
    }
}
