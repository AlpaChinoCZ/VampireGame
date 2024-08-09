using UnityEngine;

namespace VG
{
    public interface ILaunchable
    {
        /// <summary>
        /// Launch projectile from owner to target
        /// </summary>
        public void Launch(Vector3 startPosition,Vector3 targetPosition);
    }
}
