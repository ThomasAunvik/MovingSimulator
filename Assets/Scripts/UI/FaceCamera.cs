using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovingSim.UI
{
    public class FaceCamera : MonoBehaviour
    {
        private Canvas canvas;
        private Transform mainCamera;

        public void Start()
        {
            canvas = GetComponentInParent<Canvas>();
            mainCamera = canvas.worldCamera?.transform;
        }

        public void LateUpdate()
        {
            if (mainCamera != null)
            {
                Vector3 camRotation = mainCamera.eulerAngles;

                transform.eulerAngles = camRotation;
            }
        }
    }
}