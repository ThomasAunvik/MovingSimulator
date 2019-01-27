using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovingSim
{
    public class ItemDrawer : MonoBehaviour
    {
        private Animator animator;
        bool isOpen;

        public void Start()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public void ToggleDrawer()
        {
            isOpen = !isOpen;
            animator.SetBool("IsOpen", isOpen);
        }
    }
}