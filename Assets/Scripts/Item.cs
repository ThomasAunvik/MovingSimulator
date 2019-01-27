using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MovingSim
{
    [DisallowMultipleComponent]
    public class Item : MonoBehaviour, IItem
    {
        [System.Serializable]
        public struct MeshViewOffset
        {
            public float size;
            public Vector3 rotation;
        }

        public static List<Item> itemList = new List<Item>();
        private List<Item> childItems = new List<Item>();

        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        private Material[] originalMaterials;
        [SerializeField] private Material[] outlineMaterials;
        [SerializeField] private bool ignoreChildren;

        public string itemName;
        public string dialogue;
        [TextArea]
        public string description;

        [SerializeField] private MeshViewOffset viewOffset = new MeshViewOffset() { size = 1 };

        public bool destroying { get; private set; }
        public bool isKeeping { get; private set; }

        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            originalMaterials = meshRenderer.materials;

            meshFilter = GetComponent<MeshFilter>();

            itemList.Add(this);

            childItems = GetComponentsInChildren<Item>().ToList();
            childItems.RemoveAll(i => i.gameObject == gameObject);
            //if (childItems.Contains(this)) childItems.Remove(this);
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

        public bool CanSelect()
        {
            if (!ignoreChildren)
            {
                for (int i = 0; i < childItems.Count; i++)
                {
                    Item item = childItems[i];
                    if (item != null)
                    {
                        if (item.CanSelect())
                        {
                            return false;
                        }
                    }
                }
            }
            return !isKeeping && !destroying;
        }

        public MeshViewOffset GetViewOffset()
        {
            return viewOffset;
        }
    }
}
