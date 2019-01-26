using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovingSim
{
    public class Item : MonoBehaviour, IItem
    {
        public static List<Item> itemList = new List<Item>();

        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        private Material[] originalMaterials;
        [SerializeField] private Material[] outlineMaterials;

        public string itemName;
        public string dialogue;
        public string description;

        public bool destroying { get; private set; }
        public bool isKeeping { get; private set; }

        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            originalMaterials = meshRenderer.materials;

            meshFilter = GetComponent<MeshFilter>();

            itemList.Add(this);
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
            itemList.Remove(this);
            destroying = true;
            Destroy(gameObject, 0.5f);
        }

        public void Keep()
        {
            isKeeping = true;
        }

        public Material[] GetMaterials()
        {
            return originalMaterials;
        }

        public Mesh GetMesh()
        {
            return meshFilter.mesh;
        }

        public string GetName()
        {
            return itemName;
        }

        public string GetDialogue()
        {
            return dialogue;
        }

        public string GetDescription()
        {
            return description;
        }

        public bool IsDestroying()
        {
            return destroying;
        }

        public bool IsKeeping()
        {
            return isKeeping;
        }

        public Item GetItem()
        {
            return this;
        }
    }
}
