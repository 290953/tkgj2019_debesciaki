using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int acidLoads = 0;
    int waterLoads = 5;
    public int maxLoads = 5;

    public KeyCode actionKey = KeyCode.E;

    public AudioClip useWaterClip;
    public AudioClip useAcidClip;

    private Rigidbody rb;

    [SerializeField]
    private PlayerModel playerModel;

    [SerializeField] private FollowPlayer followPlayer;
    public float speed;
    Collider collision;
    GameUi gameUi;
    AudioSource audioSource;


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

        InitAudio();

    }

    void InitAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
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
        if (Input.GetKeyUp(actionKey))
        {
            HandleCollision(collision);
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

    void HandleCollision(Collider col)
    {
        if (col == null)
        {
            return;
        }
        if (col.CompareTag("Tree"))
        {
            HandleTree(col);
        }
        else if (col.CompareTag("WaterSource"))
        {
            HandleWaterSource(col);
        }
        else if (col.CompareTag("AcidSource"))
        {
            HandleAcidSource(col);
        }
        else if (col.CompareTag("Shrine"))
        {
            HandleShrine(col);
        }
    }

    void HandleWaterSource(Collider col)
    {
        Source source = col.GetComponent<Source>();
        Debug.LogWarning("adding water");
        if (AcidLoads <= 0 && WaterLoads < maxLoads)
        {
            WaterLoads += source.GetLoad();
        }
    }

    void HandleAcidSource(Collider col)
    {
        Source source = col.GetComponent<Source>();
        Debug.LogWarning("adding acid");
        if (WaterLoads <= 0 && AcidLoads < maxLoads)
        {
            AcidLoads += source.GetLoad();
        }
    }


    void HandleTree(Collider col)
    {
        Tree tree = col.GetComponent<Tree>();
        if (AcidLoads > 0)
        {
            DestroyTree(tree);
        }
        else if (WaterLoads > 0)
        {
            MakeMagicalTree(tree);
        }
    }

    void HandleShrine(Collider col)
    {
        Shrine shrine = col.GetComponent<Shrine>();
        if (WaterLoads > 0)
        {
            shrine.PutLoad();
            WaterLoads--;
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
            audioSource.PlayOneShot(useAcidClip);
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
            audioSource.PlayOneShot(useWaterClip);
        }
    }
}
