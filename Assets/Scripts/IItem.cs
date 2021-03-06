﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovingSim
{
    public interface IItem
    {
        Item GetItem();

        string GetName();
        string GetDialogue();
        string GetDescription();

        bool IsDestroying();
        bool IsKeeping();
        bool CanSelect();

        void ShowOutline();
        void HideOutline();
        void Trash();
        void Keep();
        Material[] GetMaterials();
        Mesh GetMesh();

        Item.MeshViewOffset GetViewOffset();
    }
}