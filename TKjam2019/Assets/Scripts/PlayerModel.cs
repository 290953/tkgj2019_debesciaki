using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [NonSerialized]
    public bool isMoving = false;

    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    public Rigidbody _rigidbody;

    private const string floorTag = "Floor";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Floor") && isMoving)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Floor") && isMoving)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }
}
