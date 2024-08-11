using UnityEngine;

namespace VG
{
    public interface ILaunchable
    {
        Transform StartPosition { get; }

        void Launch(Vector3 targetPosition);
    }
}
