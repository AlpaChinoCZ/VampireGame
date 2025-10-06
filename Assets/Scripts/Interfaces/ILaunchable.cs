using UnityEngine;

namespace VG
{
    public interface ILaunchable
    {
        Transform StartPosition { get; }

        GameObject Launch(Vector3 targetPosition);
    }
}
