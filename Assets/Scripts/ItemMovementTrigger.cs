using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovingSim.Player;

namespace MovingSim
{
    [DisallowMultipleComponent]
    public class ItemMovementTrigger : MonoBehaviour, IItem
    {
        public Item item { get; private set; }
        private PlayerController player;
        private Collider coll;

        void Start()
        {
            item = GetComponentInParent<Item>();
            player = FindObjectOfType<PlayerController>();
            coll = GetComponent<Collider>();
        }

        private void Update()
        {
            bool enableCollider = player.inputType == PlayerInputType.Keyboard;
            if(coll.enabled != enableCollider)
            {
                coll.enabled = enableCollider;
            }
        }

        public void ShowOutline()
        {
            item.ShowOutline();
        }

        public void HideOutline()
        {
            item.HideOutline();
        }

        public void Trash()
        {
            item.Trash();
        }

        public void Keep()
        {
            item.Keep();
        }

        public Material[] GetMaterials()
        {
            return item.GetMaterials();
        }

        public Mesh GetMesh()
        {
            return item.GetMesh();
        }

        public string GetName()
        {
            return item.GetName();
        }

        public string GetDialogue()
        {
            return item.GetDialogue();
        }

        public string GetDescription()
        {
            return item.GetDescription();
        }

        public bool IsDestroying()
        {
            return item.IsDestroying();
        }

        public bool IsKeeping()
        {
            return item.IsDestroying();
        }

        public Item GetItem()
        {
            return item;
        }

        public bool CanSelect()
        {
            return item.CanSelect();
        }

        public Item.MeshViewOffset GetViewOffset()
        {
            return item.GetViewOffset();
        }
    }
}