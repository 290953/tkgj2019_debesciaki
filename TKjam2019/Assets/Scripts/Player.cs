using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int acidLoads = 5;
    public int waterLoads = 5;

    private Rigidbody rb;
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
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Tree"))
        {
            Tree tree = col.GetComponent<Tree>();
            if (Input.GetKey(KeyCode.E))
            {
                DestroyTree(tree);
            }
            if (Input.GetKey(KeyCode.R))
            {
                MakeMagicalTree(tree);
            }
        }
    }

    void DestroyTree(Tree tree)
    {
        if (acidLoads > 0)
        {
            if (tree.SetDestroyed())
            {
                acidLoads -= 1;
            }
        }
    }

    void MakeMagicalTree(Tree tree)
    {
        if (waterLoads > 0)
        {
            if (tree.SetMagical())
            {
                waterLoads -= 1;
            }
        }
    }
}
