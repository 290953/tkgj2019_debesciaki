using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int acidLoads = 0;
    public int waterLoads = 5;
    public int maxLoads = 5;

    private Rigidbody rb;
    public float speed;
    Collider collision;

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            HandleCollision(collision, KeyCode.E);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            HandleCollision(collision, KeyCode.R);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collision = other;
    }

    private void OnTriggerExit(Collider other)
    {
        collision = null;
    }

    void HandleCollision(Collider col, KeyCode key)
    {
        if (col == null)
        {
            return;
        }
        if (col.CompareTag("Tree"))
        {
            HandleTree(col, key);
        }
        else if (col.CompareTag("WaterSource"))
        {
            HandleWaterSource(col, key);
        }
        else if (col.CompareTag("AcidSource"))
        {
            HandleAcidSource(col, key);
        }
    }

    void HandleWaterSource(Collider col, KeyCode key)
    {
        Source source = col.GetComponent<Source>();
        if (key == KeyCode.E)
        {

            Debug.LogWarning("adding water");
            if (acidLoads <= 0 && waterLoads < maxLoads)
            {
                waterLoads += source.GetLoad();
            }
        }
    }

    void HandleAcidSource(Collider col, KeyCode key)
    {
        Source source = col.GetComponent<Source>();
        if (key == KeyCode.E)
        {

            Debug.LogWarning("adding acid");
            if (waterLoads <= 0 && acidLoads < maxLoads)
            {
                acidLoads += source.GetLoad();
            }
        }
    }


    void HandleTree(Collider col, KeyCode key)
    {
        Tree tree = col.GetComponent<Tree>();
        if (key == KeyCode.E)
        {
            DestroyTree(tree);
        }
        if (key == KeyCode.R)
        {
            MakeMagicalTree(tree);
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
