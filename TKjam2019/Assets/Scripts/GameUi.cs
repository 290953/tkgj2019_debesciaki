using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public Text playerLabel;
    public Text shrineLabel;


    int acidLoads;
    int waterLoads;
    int maxLoads;

    int shrineLoads;
    int shrineMaxLoads;

    public void UpdateAcidLoads(int loads)
    {
        acidLoads = loads;
        playerLabel.text = "acid: " + acidLoads + "/" + maxLoads + " magic: " + waterLoads + "/" + maxLoads;
    }

    public void UpdateWaterLoads(int loads)
    {
        waterLoads = loads;
        playerLabel.text = "acid: " + acidLoads + "/" + maxLoads + " magic: " + waterLoads + "/" + maxLoads;
    }

    public void UpdateMaxLoads(int loads)
    {
        maxLoads = loads;
        playerLabel.text = "acid: " + acidLoads + "/" + maxLoads + " magic: " + waterLoads + "/" + maxLoads;
    }

    public void UpdateShrineMaxLoads(int loads)
    {
        shrineMaxLoads = loads;
        shrineLabel.text = "shrine: " + shrineLoads + "/" + shrineMaxLoads;
    }

    public void UpdateShrineLoads(int loads)
    {
        shrineLoads = loads;
        shrineLabel.text = "shrine: " + shrineLoads + "/" + shrineMaxLoads;
    }


}
