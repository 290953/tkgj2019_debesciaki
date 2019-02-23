using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public int loadsToActivate = 10;

    public int treesHealed = 10;

    public int loads;

    Trees trees;

    // Start is called before the first frame update
    void Start()
    {
        GameObject treesObject = GameObject.Find("Trees");
        if (treesObject != null)
        {
            trees = treesObject.GetComponent<Trees>();
        }
    }



    public void PutLoad()
    {
        loads++;
        Debug.LogWarning("shrine loads: " + loads);
        if (loads >= loadsToActivate)
        {
            Debug.LogWarning("healing trees");
            trees.HealMetalTrees(treesHealed);
            loads = 0;
        }
    }
}
