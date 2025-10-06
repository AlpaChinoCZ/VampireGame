using UnityEngine;

namespace VG
{
    public interface IMove
    {
        public Vector3 GetVelocity();
        public void SetVelocity(Vector3 velocityVector);
        public void Jump();
    }
}
