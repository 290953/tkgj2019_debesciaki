using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{

    delegate void ProcessHit(RaycastHit hit);

    void ProcessRayCast(ProcessHit processHit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            processHit(hit);
        }
    }


    void Start()
    {
        InfectFirstTree();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ProcessRayCast((hit) =>
            {
                Tree tree = hit.collider.gameObject.GetComponent<Tree>();
                if (tree != null)
                {
                    Debug.Log(tree.name);
                    tree.MyState = Tree.State.DESTROYED;
                }
            });
        }
    }

    void InfectFirstTree()
    {
        Tree[] trees = transform.GetComponentsInChildren<Tree>();
        Tree infectedTree = trees[Random.Range(0, trees.Length)];
        infectedTree.MyState = Tree.State.INFECTED;
    }
}
