using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public AudioClip loadTakenClip;
    public AudioClip sourceEmptyClip;

    public int maxLoads = 10;

    public float timeToRenew = 10f;

    int loads;

    AudioSource audioSource;

    private void Awake()
    {
        loads = maxLoads;
        InvokeRepeating("Renew", timeToRenew, timeToRenew);
        InitAudio();
    }

    void InitAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Renew()
    {
        if (loads < maxLoads)
        {
            loads += 1;
        }
    }

    public int GetLoad()
    {
        if (loads > 0)
        {
            loads--;
            audioSource.PlayOneShot(loadTakenClip);
            return 1;
        }
        else
        {
            audioSource.PlayOneShot(sourceEmptyClip);
            return 0;
        }
    }
}
