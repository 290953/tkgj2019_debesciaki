using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public Text acidText;
    public Text magicText;
    public Text treesText;
    public Text shrineText;
    public Text gameOverLabel;

    int acidLoads;
    int waterLoads;
    int maxLoads;

    int shrineLoads;
    int shrineMaxLoads;

    public void UpdateAcidLoads(int loads)
    {
        acidLoads = loads;
        acidText.text = acidLoads + "/" + maxLoads;
    }

    public void UpdateWaterLoads(int loads)
    {
        waterLoads = loads;
        acidText.text = waterLoads + "/" + maxLoads;
    }

    public void UpdateMaxLoads(int loads)
    {
        maxLoads = loads;
    }

    public void UpdateShrineMaxLoads(int loads)
    {
        shrineMaxLoads = loads;
        shrineText.text = shrineLoads + "/" + shrineMaxLoads;
    }

    public void UpdateShrineLoads(int loads)
    {
        shrineLoads = loads;
        shrineText.text = shrineLoads + "/" + shrineMaxLoads;
    }

    public void UpdateTrees(int trees)
    {
        treesText.text = trees.ToString();
    }

    public void SetGameOver()
    {
        gameOverLabel.gameObject.SetActive(true);
    }
}
