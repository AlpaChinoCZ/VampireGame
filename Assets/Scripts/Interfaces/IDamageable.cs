using System;

namespace VG
{
    public interface IDamageable
    {
        public event Action OnDamaged;
        void ApplyDamage(float damage);
    }
}
