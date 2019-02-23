using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float MaxZOffset;

    public float MaxXOffset;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Follow(Vector3 positionToFollow, float horizontalInput, float verticalInput)
    {
        transform.position = new Vector3(positionToFollow.x, transform.position.y, positionToFollow.z );
    }
}
