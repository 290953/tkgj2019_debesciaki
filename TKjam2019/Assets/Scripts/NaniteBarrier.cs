using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaniteBarrier : MonoBehaviour
{
    public Action OnNaniteBarrierFullyOpaque;
    
    [SerializeField] private MeshRenderer meshRenderer;
    private Coroutine _naniteBarrierShowCoroutine;

    public void FadeInBarrier()
    {
        StartCoroutine(IncreaseMaterialAlpha());
    }

    
    private IEnumerator IncreaseMaterialAlpha()
    {
        float currentAlpha = meshRenderer.material.GetFloat("Vector1_66923FC1");
        while (currentAlpha < 1)
        {
            currentAlpha += 0.01f;
            meshRenderer.material.SetFloat("Vector1_66923FC1", currentAlpha);
            yield return new WaitForSeconds(0.01f);
        }
        OnNaniteBarrierFullyOpaque();
        yield return new WaitForSeconds(1f);
        StartCoroutine(DecreaseMaterialAlpha());
    }
    
    private IEnumerator DecreaseMaterialAlpha()
    {
        float currentAlpha = meshRenderer.material.GetFloat("Vector1_66923FC1");
        while (currentAlpha > 0)
        {
            currentAlpha -= 0.01f;
            meshRenderer.material.SetFloat("Vector1_66923FC1", currentAlpha);
            yield return new WaitForSeconds(0.01f);
        }
        ResetState();
    }

    private void ResetState()
    {
        meshRenderer.sharedMaterial.SetFloat("Vector1_66923FC1", 0);
    }
    
    
}
