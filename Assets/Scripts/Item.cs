using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovingSim
{
    public class Item : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        private Material[] originalMaterials;
        [SerializeField] private Material[] outlineMaterials;

        public string itemName;
        public string dialogue;
        public string description;

        public bool destroying { get; private set; }

        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            originalMaterials = meshRenderer.materials;

            meshFilter = GetComponent<MeshFilter>();
        }

        public void ShowOutline()
        {
            meshRenderer.materials = outlineMaterials;
        }

        public void HideOutline()
        {
            meshRenderer.materials = originalMaterials;
        }

        public void Trash()
        {
            // TODO: SPAWN EFFECT
            destroying = true;
            Destroy(gameObject, 0.5f);
        }

        public void Keep()
        {

        }

        public Material[] GetMaterials()
        {
            return originalMaterials;
        }

        public Mesh GetMesh()
        {
            return meshFilter.mesh;
        }
    }
}
