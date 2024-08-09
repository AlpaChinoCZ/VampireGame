using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    /// <summary>
    /// Camera which follows given object
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [Tooltip("Init position of the camera")]
        [SerializeField] private Transform cameraTransform;

        private Vector3 cameraOffset;
        private Camera camera;

        private void Awake()
        {
            camera = Camera.main;
            
            Assert.IsNotNull(camera, $"{gameObject} main camera is null");
        }

        private void Start()
        {
            cameraOffset = cameraTransform.position - transform.position;
            camera.transform.position = cameraTransform.position;
            camera.transform.LookAt(transform.position);
        }
        
        private void LateUpdate()
        {
            camera.transform.position =  transform.position + cameraOffset;
        }
    }
}
