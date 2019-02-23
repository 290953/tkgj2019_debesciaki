using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public int loadsToActivate = 10;

    public int treesHealed = 10;

    int loads;

    GameUi gameUi;

    int Loads
    {
        get
        {
            return loads;
        }
        set
        {
            loads = value;
            if (gameUi != null)
            {
                gameUi.UpdateShrineLoads(loads);
            }
        }
    }



    Trees trees;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void PutLoad()
    {
        Loads++;
        Debug.LogWarning("shrine loads: " + loads);
        if (Loads >= loadsToActivate)
        {
            Debug.LogWarning("healing trees");
            trees.HealMetalTrees(treesHealed);
            Loads = 0;
        }
    }
}
