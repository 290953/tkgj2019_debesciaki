using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color acidColor;
    [SerializeField] private Color holyWaterColor;


    public void Start()
    {
        ChangeColorToDefault();
    }
    
    public void ChangeColorToAcid()
    {
        meshRenderer.material.SetColor("Color_44AE8409", acidColor);
    }

    public void ChangeColorToWater()
    {
        meshRenderer.material.SetColor("Color_44AE8409", holyWaterColor);
    }

    public void ChangeColorToDefault()
    {
        meshRenderer.material.SetColor("Color_44AE8409", Color.white);
    }
}
