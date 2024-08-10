using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace VG
{
    /// <summary>
    /// Camera which tracking given object
    /// </summary>
    public class TrackingCamera : MonoBehaviour
    {
        [Tooltip("Init position of the camera")]
        [SerializeField] private Transform cameraTransform;

        private Vector3 cameraOffset;
        private Camera trackingCamera;

        private void Awake()
        {
            trackingCamera = Camera.main;
            
            Assert.IsNotNull(trackingCamera, $"{gameObject} main camera is null");
        }

        private void Start()
        {
            cameraOffset = cameraTransform.position - transform.position;
            trackingCamera.transform.position = cameraTransform.position;
            trackingCamera.transform.LookAt(transform.position);
        }
        
        private void LateUpdate()
        {
            trackingCamera.transform.position =  transform.position + cameraOffset;
        }
    }
}
