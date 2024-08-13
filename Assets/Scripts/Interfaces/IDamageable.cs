using UnityEngine.Events;

namespace VG
{
    public interface IDamageable
    {
        UnityEvent OnDamaged { get; }
        void ApplyDamage(float damage);
    }
}
