using System;
using UnityEngine;

namespace VG
{
    public class SinMovement : MonoBehaviour
    {
        [Tooltip("Height of the wave")]
        [SerializeField] private float waveHeight = 2.0f;
        [Tooltip("Frequency of the wave")]
        [SerializeField] private float waveFrequency = 1.0f;

        private Vector3 initialPosition;
        
        private void Start()
        {
            initialPosition = transform.position;
        }

        private void Update()
        {
            var yOffset = Mathf.Sin(Time.time * waveFrequency) * waveHeight;
            var newPosition = transform.position;
            newPosition.y = initialPosition.y + yOffset + waveHeight;
            transform.position = newPosition;
        }
    }
}
