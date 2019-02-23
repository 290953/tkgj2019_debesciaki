using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public int maxLoads = 10;

    public float timeToRenew = 10f;

    int loads;

    private void Awake()
    {
        loads = maxLoads;
        InvokeRepeating("Renew", timeToRenew, timeToRenew);
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
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
