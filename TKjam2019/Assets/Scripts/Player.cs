﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int acidLoads = 0;
    public int waterLoads = 0;
    public int maxLoads = 5;

    public KeyCode actionKey = KeyCode.E;

    public AudioClip useWaterClip;
    public AudioClip useAcidClip;
    public AudioClip sinkClip;

    private Rigidbody rb;

    [SerializeField]
    private PlayerModel playerModel;

    [SerializeField] private FollowPlayer followPlayer;
    public float speed;
    Collider collision;
    GameUi gameUi;
    AudioSource audioSource;
    bool isDead;


    int WaterLoads
    {
        get
        {
            return waterLoads;
        }
        set
        {

            waterLoads = value;
            if (waterLoads == 0 && acidLoads == 0)
            {
                playerModel.ChangeColorToDefault();
            }
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
            if (waterLoads == 0 && acidLoads == 0)
            {
                playerModel.ChangeColorToDefault();
            }
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
            gameUi.UpdateMaxLoads(maxLoads);
            gameUi.UpdateWaterLoads(waterLoads);
            gameUi.UpdateAcidLoads(acidLoads);

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
        if (isDead)
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (playerModel != null)
        {
            
 
        }
        Quaternion lol = Quaternion.Euler(0, followPlayer.transform.eulerAngles.y, 0);

           rb.velocity =  lol * new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
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
        if (other.CompareTag("Water"))
        {
            gameUi.SetGameOver();
            isDead = true;
            audioSource.PlayOneShot(sinkClip);
        }
        else
        {
            collision = other;
        }
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
        if (WaterLoads < maxLoads)
        {
            WaterLoads += source.GetLoad();
            AcidLoads = 0;
            playerModel.ChangeColorToWater();
        }
    }

    void HandleAcidSource(Collider col)
    {
        Source source = col.GetComponent<Source>();
        Debug.LogWarning("adding acid");
        if (AcidLoads < maxLoads)
        {
            AcidLoads += source.GetLoad();
            WaterLoads = 0;
            playerModel.ChangeColorToAcid();
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
