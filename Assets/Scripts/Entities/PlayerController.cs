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

            
            
            playerInputAction = new PlayerInputAction();
        }
        
        private void Update()
        {
            if (RaycastCameraToMouse(out var hitResult, mouseMovementLayers))
            {
                player.MovementController.SetLookTarget(hitResult.point);
            }
        }

        private void OnEnable()
        {
            playerInputAction.Enable();
            playerInputAction.Player.Move.performed += SetMoveInput;
            playerInputAction.Player.Move.canceled += SetMoveInput;
            playerInputAction.Player.Jump.started += Jump;
            playerInputAction.Player.MouseLook.performed += MouseLook;
            playerInputAction.Player.Shoot.started += Shoot;
        }
        
        private void OnDisable()
        {
            playerInputAction.Player.Move.performed -= SetMoveInput;
            playerInputAction.Player.Move.canceled -= SetMoveInput;
            playerInputAction.Player.Jump.started -= Jump;
            playerInputAction.Player.MouseLook.performed -= MouseLook;
            playerInputAction.Player.Shoot.started -= Shoot;
            playerInputAction.Disable();
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
        
        private void Shoot(InputAction.CallbackContext context)
        {
            if(RaycastCameraToMouse(out RaycastHit hitResult, clickableLayers))
            {
                player.ShootingComponent.Launch(player.transform.position, hitResult.transform.position);
            }
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
