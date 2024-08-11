using System;
using System.Collections;
using System.Collections.Generic;
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
        
        public void Init(Player player, Camera camera)
        {
            this.player = player;
            playerCamera = camera;
        }
        
        public void SwitchActionMap(ActionMap newActionMap)
        {
            foreach (var map in playerInputAction.asset.actionMaps)
            {
                map.Disable();
            }

            playerInputAction.FindAction(actionMaps[newActionMap])?.Enable();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            actionMaps = new Dictionary<ActionMap, string>
            {
                { ActionMap.Player, "Player" },
                { ActionMap.UI, "UI" }
            };
            rapidFireWait = new WaitForSeconds(1f / player.FireRate);
            playerInputAction = new PlayerInputAction();
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
            playerInputAction.Enable();
            playerInputAction.Player.Move.performed += SetMoveInput;
            playerInputAction.Player.Move.canceled += SetMoveInput;
            playerInputAction.Player.Jump.started += Jump;
            playerInputAction.Player.MouseLook.performed += MouseLook;
            
            playerInputAction.Player.Shoot.started += Fire;
        }
        
        private void OnDisable()
        {
            playerInputAction.Player.Move.performed -= SetMoveInput;
            playerInputAction.Player.Move.canceled -= SetMoveInput;
            playerInputAction.Player.Jump.started -= Jump;
            playerInputAction.Player.MouseLook.performed -= MouseLook;
            
            playerInputAction.Player.Shoot.started -= Fire;
            
            playerInputAction.Disable();
            
            StopAllCoroutines();
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
