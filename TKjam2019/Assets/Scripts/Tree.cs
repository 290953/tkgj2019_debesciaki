using System.Collections;
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


    [Space] [Header("Default")]
    private Transform startTransform;
    [SerializeField] private Mesh treeMesh;
    [SerializeField] private Material treeMaterial;
    
    [Space] [SerializeField] private NaniteBarrier naniteBarrierPrefab;
    [SerializeField] private Mesh infectedMesh;
    [SerializeField] private Material infectedMaterial;
    [SerializeField] private Vector3 infectedScale;
    [SerializeField] private NaniteBarrier naniteBarrier;

    [Space] [SerializeField] private Material metalMaterial;

    [Space] [SerializeField] private Material trunkMaterial;
    [SerializeField] private Mesh trunkMesh;
    
    

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
                    StartCoroutine(Grow());
                    Invoke("SetNormal", timeToNormal);
                    break;
                case State.NORMAL:
                    ChangeModelToTree();
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
                    naniteBarrier.FadeInBarrier();
                    Invoke("Infect", timeToInfect);
                    break;
                case State.METAL:
                    meshRenderer.material = metalMaterial;
                    break;
                case State.DESTROYED:
                    CapsuleCollider theCollider = GetComponent<CapsuleCollider>();
                    theCollider.enabled = false;
                    ChangeModelToTrunk();
                    break;
            }

            parent.OnTreeStateChanged();

        }
    }

    State state;
    MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    int infectLoads;
    int spreadGrowLoads;
    Trees parent;

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
        parent = GetComponentInParent<Trees>();
        startTransform = transform;
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        naniteBarrier = Instantiate(naniteBarrierPrefab);
        naniteBarrier.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
        naniteBarrier.OnNaniteBarrierFullyOpaque += ChangeModelToNanite;
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

    void ChangeModelToNanite()
    {
        meshFilter.mesh = infectedMesh;
        meshRenderer.material = infectedMaterial;
        transform.localScale = infectedScale;
    }

    void ChangeModelToTree()
    {
        meshFilter.mesh = treeMesh;
        meshRenderer.material = treeMaterial;
        transform.position = startTransform.position;
        transform.localScale = startTransform.localScale;
    }

    void ChangeModelToTrunk()
    {
        meshFilter.mesh = trunkMesh;
        meshRenderer.material = trunkMaterial;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.01f, transform.localPosition.z);
    }

    IEnumerator Grow()
    {
        float currentYScale = 0;
        while (currentYScale < 3f)
        {
            currentYScale += 0.01f;
            transform.localScale = new Vector3(transform.localScale.x, currentYScale, transform.localScale.z);
            yield return new WaitForSeconds(0.01f);
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
