﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using MovingSim.UI;

namespace MovingSim.Player
{
    public enum PlayerInputType
    {
        Touch,
        Mouse,
        Keyboard
    }
    
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class PlayerController : MonoBehaviour
    {
        private Camera cam;
        private Transform camTransform;
        private ThirdPersonCharacter character;
        public UnityEngine.AI.NavMeshAgent aiAgent { get; private set; }
        public Transform target;
        public float moveSpeedMultiplier = 1f;

        private Vector3 camForward;
        private Vector3 move;

        public PlayerInputType inputType { get; private set; }

        private IItem currentItemTarget;

        private Ray mouseRay;

        [SerializeField] private UIManager uiManager;

        [SerializeField] private bool disableCharacter;
        public Transform defaultParent;

        

        private void Start()
        {
            character = GetComponent<ThirdPersonCharacter>();
            if (Camera.main != null)
            {
                cam = Camera.main;
                camTransform = cam.transform;
            }
            aiAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

            aiAgent.updateRotation = true;
            aiAgent.updatePosition = true;

            if(uiManager == null)
            {
                uiManager = FindObjectOfType<UIManager>();
            }
        }

        private void Update()
        {
            
            switch (inputType)
            {
                case PlayerInputType.Mouse:
                    UpdateMouseMovement();
                    break;
                case PlayerInputType.Touch:
                    UpdateTouchMovement();
                    break;
            }
        }

        private void FixedUpdate()
        {
            //if(uiOpen) return;

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            if (Mathf.Abs(v) > 0 || Mathf.Abs(h) > 0)
            {
                inputType = PlayerInputType.Keyboard;
            }else if (Input.GetMouseButton(0) && (uiManager == null || !uiManager.uiOpen)) // TODO: Check if user also has UI open.
            {
                inputType = PlayerInputType.Mouse;
            }
            else if(Input.touchCount > 0 && (uiManager == null || !uiManager.uiOpen))
            {
                inputType = PlayerInputType.Touch;
            }

            if ((uiManager == null || !uiManager.uiOpen) && !disableCharacter)
            {
                switch (inputType)
                {
                    case PlayerInputType.Keyboard:
                        UpdateKeyboardMovement(v, h);
                        break;
                }
            }
        }

        private void UpdateKeyboardMovement(float v, float h)
        {
            aiAgent.isStopped = true;
            if (cam != null)
            {
                camForward = Vector3.Scale(camTransform.forward, new Vector3(1, 0, 1)).normalized;
                move = v * camForward + h * camTransform.right;
            }
            else
            {
                move = v * Vector3.forward + h * Vector3.right;
            }

            character.Move(move * moveSpeedMultiplier, false, false);
        }

        private void UpdateMouseMovement()
        {
            if (uiManager == null || !uiManager.uiOpen)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SetScreenTargetPosition(Input.mousePosition);
                }
            }

            UpdateAIMovement();
        }

        private void UpdateTouchMovement()
        {
            if (uiManager == null || !uiManager.uiOpen)
            {
                if (Input.touches.Length > 0)
                {
                    Touch touch = Input.touches[0];
                    if (touch.phase == TouchPhase.Began)
                    {
                        SetScreenTargetPosition(touch.position);
                    }
                }
            }
            
            UpdateAIMovement();
        }

        private void UpdateAIMovement()
        {
            if (disableCharacter) return;

            if (target != null && target.position != Vector3.zero)
            {
                if (target != null)
                    aiAgent.SetDestination(target.position);
                
                if (aiAgent.remainingDistance > aiAgent.stoppingDistance)
                {
                    aiAgent.isStopped = false;
                    character.Move(aiAgent.desiredVelocity, false, false);
                }
                else
                {
                    aiAgent.isStopped = true;
                    character.Move(Vector3.zero, false, false);
                }
            }
            else
            {
                aiAgent.isStopped = true;
                character.Move(Vector3.zero, false, false);
            }
        }

        private void SetScreenTargetPosition(Vector3 screenPosition)
        {
            Vector3 worldMousePos = cam.ScreenToWorldPoint(screenPosition);
            if (target == null)
            {
                target = new GameObject().transform;
            }

            RaycastHit hit;
            mouseRay = new Ray(worldMousePos, camTransform.forward);
            if (Physics.Raycast(mouseRay, out hit))
            {
                target.position = hit.point;

                IItem item = hit.transform.GetComponent<IItem>();
                ItemDrawer drawer = hit.transform.GetComponent<ItemDrawer>();
                if (item != null)
                {
                    if (item == currentItemTarget)
                    {
                        OpenThrowOrKeepUIOnCurrentItem();
                    }
                    else if (item.CanSelect())
                    {
                        if (currentItemTarget != null)
                        {
                            currentItemTarget.HideOutline();
                        }
                        item.ShowOutline();
                        currentItemTarget = item;

                        if (uiManager != null) uiManager.OpenDialogue(item);
                    }
                }
                else if(drawer != null)
                {
                    drawer.ToggleDrawer();
                }
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        
        public void UnselectItem()
        {
            if (currentItemTarget != null)
            {
                if (uiManager != null) uiManager.CloseDialogue();
                currentItemTarget.HideOutline();
                currentItemTarget = null;
            }
        }

        public void OpenThrowOrKeepUIOnCurrentItem()
        {
            if (uiManager != null && currentItemTarget.CanSelect()) uiManager.OpenThrowOrKeep(currentItemTarget);
            currentItemTarget.HideOutline();

            // INVOKE
        }

        public void OnTriggerEnter(Collider collider)
        {
            ItemMovementTrigger item = collider.transform.GetComponent<ItemMovementTrigger>();
            if (item != null)
            {
                if(currentItemTarget != null) currentItemTarget.HideOutline();

                item.ShowOutline();
                currentItemTarget = item.item;
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            ItemMovementTrigger item = collider.transform.GetComponent<ItemMovementTrigger>();
            if (item != null)
            {
                if (currentItemTarget == item.item)
                {
                    currentItemTarget = null;
                }
                item.HideOutline();
            }
        }

        public void OnDrawGizmos()
        {
            if(mouseRay.origin != Vector3.zero) Gizmos.DrawRay(mouseRay);
        }
    }
}