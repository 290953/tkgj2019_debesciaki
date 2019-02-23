using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int acidLoads = 0;
    int waterLoads = 5;
    public int maxLoads = 5;

    private Rigidbody rb;

    [SerializeField]
    private PlayerModel playerModel;

    [SerializeField] private FollowPlayer followPlayer;
    public float speed;
    Collider collision;
    GameUi gameUi;

    int WaterLoads
    {
        get
        {
            return waterLoads;
        }
        set
        {
            waterLoads = value;
            if (gameUi != null)
            {
                gameUi.UpdateWaterLoads(waterLoads);
            }
        }
    }

    int AcidLoads
    {
        get
        {
            return acidLoads;
        }
        set
        {
            acidLoads = value;
            if (gameUi != null)
            {
                gameUi.UpdateAcidLoads(acidLoads);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        GameObject gameUiObject = GameObject.Find("GameUi");
        if (gameUiObject != null)
        {
            gameUi = gameUiObject.GetComponent<GameUi>();
            gameUi.UpdateWaterLoads(waterLoads);
            gameUi.UpdateAcidLoads(acidLoads);
            gameUi.UpdateMaxLoads(maxLoads);
        }

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (playerModel != null)
        {
            playerModel.isMoving = horizontal != 0 || vertical != 0;

            //        playerModel._rigidbody.velocity = new Vector3(horizontal * speed, playerModel._rigidbody.velocity.y, vertical * speed);
            playerModel.transform.position = new Vector3(transform.position.x, playerModel.transform.position.y, transform.position.z);
        }
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);

        if (followPlayer != null)
        {
            followPlayer.Follow(transform.position, horizontal, vertical);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            HandleCollision(collision, KeyCode.E);
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
        else if (col.CompareTag("Shrine"))
        {
            HandleShrine(col, key);
        }
    }

    void HandleWaterSource(Collider col, KeyCode key)
    {
        Source source = col.GetComponent<Source>();
        if (key == KeyCode.E)
        {

            Debug.LogWarning("adding water");
            if (AcidLoads <= 0 && WaterLoads < maxLoads)
            {
                WaterLoads += source.GetLoad();
            }
        }
    }

    void HandleAcidSource(Collider col, KeyCode key)
    {
        Source source = col.GetComponent<Source>();
        if (key == KeyCode.E)
        {

            Debug.LogWarning("adding acid");
            if (WaterLoads <= 0 && AcidLoads < maxLoads)
            {
                AcidLoads += source.GetLoad();
            }
        }
    }


    void HandleTree(Collider col, KeyCode key)
    {
        Tree tree = col.GetComponent<Tree>();
        if (key == KeyCode.E)
        {
            if (AcidLoads > 0)
            {
                DestroyTree(tree);
            }
            else if (WaterLoads > 0)
            {
                MakeMagicalTree(tree);
            }
        }
    }

    void HandleShrine(Collider col, KeyCode key)
    {
        Shrine shrine = col.GetComponent<Shrine>();
        if (key == KeyCode.E)
        {
            if (WaterLoads > 0)
            {
                shrine.PutLoad();
                WaterLoads--;
            }
        }
    }

    void DestroyTree(Tree tree)
    {
        if (AcidLoads > 0)
        {
            if (tree.SetDestroyed())
            {
                AcidLoads -= 1;
            }
        }
    }

    void MakeMagicalTree(Tree tree)
    {
        if (WaterLoads > 0)
        {
            if (tree.SetMagical())
            {
                WaterLoads -= 1;
            }
        }
    }
}
