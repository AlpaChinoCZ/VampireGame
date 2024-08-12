using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace VG
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask clickableLayers;
        [SerializeField] private LayerMask mouseMovementLayers;
            
        private PlayerInputAction playerInputAction;
        private Camera playerCamera;
        private Dictionary<ActionMap, string> actionMaps;
        private Player player;
        private Rigidbody playerBody;
        private Vector2 mouseLook;

        private WaitForSeconds rapidFireWait;
        private bool isFiring = false;

        /// <summary>
        /// Initialize Player controller input system
        /// </summary>
        public void InitInput()
        {
            playerInputAction ??= new PlayerInputAction();
            actionMaps = new Dictionary<ActionMap, string>
            {
                { ActionMap.Player, "Player" },
                { ActionMap.UI, "UI" }
            };
            AddInputListeners();
        }
        /// <summary>
        /// Initialize player controller components
        /// </summary>
        public void Init(Player player, Camera camera)
        {
            this.player = player;
            playerCamera = camera;
            rapidFireWait = new WaitForSeconds(1f / player.FireRate);
        }
        
        /// <summary>
        /// Switch to givet action map - only one is enabled
        /// </summary>
        public void SwitchActionMap(ActionMap newActionMap)
        {
            foreach (var map in playerInputAction.asset.actionMaps)
            {
                map.Disable();
            }

            var actiomap = actionMaps[newActionMap];
            playerInputAction.asset.FindActionMap(actiomap).Enable();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        private void Update()
        {
            if (RaycastCameraToMouse(out var hitLookResult, mouseMovementLayers))
            {
                player.MovementController.SetLookTarget(hitLookResult.point);
            }

            if (player.NearestObjects.Count > 0 && !isFiring)
            {
                StartCoroutine(RapidFireCoroutine());
            }
        }

        private void OnEnable()
        {
            AddInputListeners();
        }
        
        private void OnDisable()
        {
            RemoveInputListeners();
            StopAllCoroutines();
        }

        private void AddInputListeners()
        {
            if (playerInputAction != null)
            {
                playerInputAction.Player.Move.performed += SetMoveInput;
                playerInputAction.Player.Move.canceled += SetMoveInput;
                playerInputAction.Player.Jump.started += Jump;
                playerInputAction.Player.MouseLook.performed += MouseLook;
            
                playerInputAction.Player.Shoot.started += Fire;
            }
        }

        private void RemoveInputListeners()
        {
            if (playerInputAction != null)
            {
                playerInputAction.Player.Move.performed -= SetMoveInput;
                playerInputAction.Player.Move.canceled -= SetMoveInput;
                playerInputAction.Player.Jump.started -= Jump;
                playerInputAction.Player.MouseLook.performed -= MouseLook;
            
                playerInputAction.Player.Shoot.started -= Fire;
            }
        }
        
        private void SetMoveInput(InputAction.CallbackContext context)
        {
            player.MovementController.SetVelocity(context.ReadValue<Vector2>());
        }
        
        private void Jump(InputAction.CallbackContext context)
        {
            player.MovementController.Jump();
        }
        
        private void MouseLook(InputAction.CallbackContext context)
        {
            mouseLook = context.ReadValue<Vector2>();
        }
        
        private void Fire(InputAction.CallbackContext context)
        {
            if (RaycastCameraToMouse(out var hitFireResult, clickableLayers))
            {
                StopAllCoroutines();
                isFiring = false;
                player.FireComponent.Launch(hitFireResult.transform.position);
            }
        }

        private IEnumerator RapidFireCoroutine()
        {
            isFiring = true;
            
            while (player.NearestObjects.Count > 0)
            {
                player.FireComponent.Launch(player.GetNearestObject().position);
                yield return rapidFireWait;
            }

            isFiring = false;
        }
        
        private bool RaycastCameraToMouse(out RaycastHit hitResult, LayerMask layers)
        {
            var mouseRay = playerCamera.ScreenPointToRay(mouseLook);
            return Physics.Raycast(mouseRay, out hitResult, Mathf.Infinity, layers);
        }
        
        public enum ActionMap
        {
            UI,
            Player
        }
    }
}