using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Color normalColor = Color.green;
    public Color infectedColor = Color.blue;
    public Color naniteColor = Color.red;

    public float timeToNanite = 5f;
    public float timeToInfect = 2f;
    public float infectRadius = 3f;

    public enum State
    {
        NORMAL,
        INFECTED,
        NANITE
    }


    private State state;

    public State MyState
    {
        get
        {
            return state;
        }
        set
        {
            if (state == value)
            {
                return;
            }
            state = value;

            switch (state)
            {
                case State.NORMAL:
                    meshRenderer.material.color = normalColor;
                    break;
                case State.INFECTED:
                    meshRenderer.material.color = infectedColor;
                    Invoke("SetToNaniteState", timeToNanite);
                    break;
                case State.NANITE:
                    meshRenderer.material.color = naniteColor;
                    Invoke("Infect", timeToInfect);
                    break;

            }

        }
    }

    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        MyState = State.NORMAL;
    }

    void SetToNaniteState()
    {
        MyState = State.NANITE;
    }

    void Infect()
    {
        Tree[] trees = transform.parent.GetComponentsInChildren<Tree>();
        Debug.Log("trees: " + trees.Length);
        List<Tree> treesInRange = new List<Tree>();
        foreach(Tree tree in trees)
        {
            float distance = Vector3.Magnitude(transform.position - tree.transform.position);
            if (distance < infectRadius && tree != this)
            {
                treesInRange.Add(tree);
            }
        }
        Debug.Log("trees in range: " + treesInRange.Count);
        if (treesInRange.Count > 0)
        {
            Tree treeToInfect = treesInRange[Random.Range(0, treesInRange.Count - 1)];
            if (treeToInfect.MyState == State.NORMAL)
            {
                treeToInfect.MyState = State.INFECTED;
            }
        }
        Invoke("Infect", timeToInfect);


    }


}
