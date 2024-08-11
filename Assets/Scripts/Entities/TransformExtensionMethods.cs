using System;
using UnityEngine;

namespace VG
{
    public static class TransformExtensionMethods
    {
        /// <summary>
        /// Smooth rotate of transform towards target in Y axis
        /// </summary>
        public static void SmoothLookAt(this Transform tr, Vector3 targetPosition, float rotationSpeed)
        {
            var lookPosition = targetPosition - tr.position;
            lookPosition.y = 0;
            var rotation = Quaternion.LookRotation(lookPosition);
            
            tr.rotation = Quaternion.Slerp(tr.rotation, rotation, rotationSpeed);
        }
    }
}