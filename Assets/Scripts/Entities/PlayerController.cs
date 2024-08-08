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
        [SerializeField] private Player player;
        [SerializeField] private LayerMask clickableLayers;
        
        public static PlayerController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerController>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<PlayerController>();
                    }
                }
                return instance;
            }
        }

        public Vector2 moveInput = new Vector2(0,0);
            
        private static PlayerController instance;
        private PlayerInputAction playerInputAction;
        private Camera playerCamera;
        private Dictionary<ActionMap, string> actionMaps;

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
            if (instance != null) Destroy(this);
            DontDestroyOnLoad(this);
            
            actionMaps = new Dictionary<ActionMap, string>
            {
                { ActionMap.Player, "Player" },
                { ActionMap.UI, "UI" }
            };
            playerCamera = Camera.main;

            playerInputAction = new PlayerInputAction();
            
            Assert.IsNotNull(player, $"{gameObject} player is null.");
            Assert.IsNotNull(playerCamera, $"{gameObject} main Player Camera is null.");
        }
        
        private void OnEnable()
        {
            playerInputAction.Enable();
            playerInputAction.Player.Move.performed += SetMoveInput;
            playerInputAction.Player.Move.canceled += SetMoveInput;
            playerInputAction.Player.Shoot.performed += Shoot;
        }
        
        private void OnDisable()
        {
            playerInputAction.Player.Move.performed -= SetMoveInput;
            playerInputAction.Player.Move.canceled -= SetMoveInput;
            playerInputAction.Disable();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        private void SetMoveInput(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            var initPos = player.transform.position;
            var mouseRay = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(mouseRay, out RaycastHit hit, 500f, clickableLayers))
            {
                player.LaunchProjectile.Launch(player.transform.position, hit.transform.position);
            }
        }

        private void UpdateVelocity()
        {
            player.Body.velocity = new Vector3(moveInput.x * player.MovementSpeed, player.Body.velocity.y, moveInput.y * player.MovementSpeed);
        }
        
        public enum ActionMap
        {
            UI,
            Player
        }
    }
}
