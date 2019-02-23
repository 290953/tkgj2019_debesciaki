using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Tree : MonoBehaviour
{
    public Color growingColor = Color.yellow;
    public Color normalColor = Color.green;
    public Color magicalColor = Color.magenta;
    public Color infectedColor = Color.blue;
    public Color naniteColor = Color.red;
    public Color metalColor = Color.gray;
    public Color destroyedColor = Color.black;

    public float timeToNanite = 5f;
    public float timeToInfect = 2f;
    public float timeToSpreadGrow = 2f;
    public float timeToNormal = 5f;
    public float infectRadius = 3f;
    public float spreadGrowRadius = 3f;
    public int maxInfectLoads = 5;
    public int maxSpreadGrowLoads = 5;

    public enum State
    {
        GROWING,
        NORMAL,
        MAGICAL,
        INFECTED,
        NANITE,
        METAL,
        DESTROYED
    }

    public State MyState
    {
        get
        {
            return state;
        }
        private set
        {
            if (state == value)
            {
                return;
            }
            state = value;

            switch (state)
            {
                case State.GROWING:
                    meshRenderer.material.color = growingColor;
                    Invoke("SetNormal", timeToNormal);
                    break;
                case State.NORMAL:
                    meshRenderer.material.color = normalColor;
                    break;
                case State.MAGICAL:
                    meshRenderer.material.color = magicalColor;
                    Invoke("SpreadGrow", timeToSpreadGrow);
                    break;
                case State.INFECTED:
                    meshRenderer.material.color = infectedColor;
                    Invoke("SetNanite", timeToNanite);
                    break;
                case State.NANITE:
                    meshRenderer.material.color = naniteColor;
                    Invoke("Infect", timeToInfect);
                    break;
                case State.METAL:
                    meshRenderer.material.color = metalColor;
                    break;
                case State.DESTROYED:
                    CapsuleCollider theCollider = GetComponent<CapsuleCollider>();
                    theCollider.enabled = false;
                    meshRenderer.material.color = destroyedColor;
                    break;
            }

        }
    }

    State state;
    MeshRenderer meshRenderer;
    int infectLoads;
    int spreadGrowLoads;


    public bool SetDestroyed()
    {
        if (MyState == State.NORMAL || MyState == State.INFECTED)
        {
            MyState = State.DESTROYED;
            return true;
        }
        return false;

    }

    public void SetInfected()
    {
        if (MyState == State.NORMAL)
        {
            MyState = State.INFECTED;
        }
    }

    public bool SetMagical()
    {
        if(MyState == State.NORMAL || MyState == State.INFECTED)
        {
            MyState = State.MAGICAL;
            spreadGrowLoads = maxSpreadGrowLoads;
            return true;
        }
        return false;
    }

    public void SetGrowing()
    {
        if (MyState == State.DESTROYED)
        {
            MyState = State.GROWING;
        }
    }

    public void SetNormal()
    {
        if (MyState == State.GROWING || MyState == State.METAL)
        {
            MyState = State.NORMAL;
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        MyState = State.NORMAL;
    }

    void SetNanite()
    {
        if (MyState == State.INFECTED)
        {
            MyState = State.NANITE;
            infectLoads = maxInfectLoads;
        }
    }

    void Infect()
    {
        if (MyState != State.NANITE)
        {
            return;
        }
        Tree[] treesInRange = GetTreesInRange(infectRadius).
            Where(x => x.MyState == State.NORMAL).ToArray();

        if (treesInRange.Length > 0)
        {
            Tree treeToInfect = treesInRange[Random.Range(0, treesInRange.Length)];
            treeToInfect.SetInfected();
        }
        infectLoads -= 1;
        if (infectLoads > 0)
        {
            Invoke("Infect", timeToInfect);
        }
        else
        {
            MyState = State.METAL;
        }
    }

    void SpreadGrow()
    {
        if (MyState != State.MAGICAL)
        {
            return;
        }
        Tree[] treesInRange = GetTreesInRange(infectRadius).
            Where(x => x.MyState == State.DESTROYED).ToArray();
        if (treesInRange.Length > 0)
        {
            Tree treeToInfect = treesInRange[Random.Range(0, treesInRange.Length)];
            treeToInfect.SetGrowing();
        }
        spreadGrowLoads -= 1;
        if (spreadGrowLoads > 0)
        {
            Invoke("SpreadGrow", timeToSpreadGrow);
        }
        else
        {
            MyState = State.NORMAL;
        }
    }

    List<Tree> GetTreesInRange(float range)
    {
        Tree[] trees = transform.parent.GetComponentsInChildren<Tree>();
        List<Tree> treesInRange = new List<Tree>();
        foreach (Tree tree in trees)
        {
            float distance = Vector3.Magnitude(transform.position - tree.transform.position);
            if (distance < range && tree != this)
            {
                treesInRange.Add(tree);
            }
        }
        return treesInRange;
    }


}
