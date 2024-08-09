using System;

namespace VG
{
    public interface IHealth
    {
        public event Action OnHealthChanged;
        public event Action OnHealed;
        public event Action OnDead;
        
        public void Heal(float amount);
    }
}
