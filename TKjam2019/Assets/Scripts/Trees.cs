using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    void Start()
    {
        InfectFirstTree();
    }

    void Update()
    {
        
    }

    void InfectFirstTree()
    {
        Tree[] trees = transform.GetComponentsInChildren<Tree>();
        Tree infectedTree = trees[Random.Range(0, trees.Length)];
        infectedTree.MyState = Tree.State.INFECTED;
    }
}
