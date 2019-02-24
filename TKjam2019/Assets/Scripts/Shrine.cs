using UnityEngine;

public class Shrine : MonoBehaviour
{
    public AudioClip loadPutClip;
    public AudioClip treesHealedClip;
    public Transform waterLevel;
    public Vector3 waterLevelDefaultPosition;
    public GameObject WaterRingExplosion;

    public int loadsToActivate = 10;

    public int treesHealed = 10;

    int loads;

    GameUi gameUi;
    AudioSource audioSource;

    int Loads
    {
        get { return loads; }
        set
        {
            loads = value;
            if (gameUi != null)
            {
                gameUi.UpdateShrineLoads(loads);
            }
        }
    }



    Trees trees = null;

    // Start is called before the first frame update
    void Start()
    {
        waterLevelDefaultPosition = waterLevel.transform.localPosition;
        GameObject treesObject = GameObject.Find("Trees");
        if (treesObject != null)
        {
            trees = treesObject.GetComponent<Trees>();
        }

        GameObject gameUiObject = GameObject.Find("GameUi");
        if (gameUiObject != null)
        {
            gameUi = gameUiObject.GetComponent<GameUi>();
            gameUi.UpdateShrineMaxLoads(loadsToActivate);

        }

        InitAudio();
    }

    void InitAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PutLoad()
    {
        Loads++;
        waterLevel.localPosition = new Vector3(waterLevel.transform.localPosition.x,
            waterLevel.transform.localPosition.y + 0.006f, waterLevel.transform.localPosition.z);
        Debug.LogWarning("shrine loads: " + loads);
            Debug.LogWarning("healing trees");
            if (Loads >= loadsToActivate)
            {
                waterLevel.transform.localPosition = waterLevelDefaultPosition;
                WaterRingExplosion.GetComponent<ParticleSystem>().Play();
                FindObjectOfType<Trees>().HealMetalTrees(treesHealed);
                Loads = 0;
                audioSource.PlayOneShot(treesHealedClip);
            }
            else
            {
                audioSource.PlayOneShot(loadPutClip);
            }
    }
}