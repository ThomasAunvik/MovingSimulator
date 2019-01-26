using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private MeshRenderer renderer;

    private Material[] originalMaterials;
    [SerializeField] private Material[] outlineMaterials;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        originalMaterials = renderer.materials;
    }

    public void ShowOutline()
    {
        renderer.materials = outlineMaterials;
    }

    public void HideOutline()
    {
        renderer.materials = originalMaterials;
    }
}
