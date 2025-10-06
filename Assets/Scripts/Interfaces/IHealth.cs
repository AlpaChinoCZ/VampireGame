using UnityEngine.Events;

namespace VG
{
    public interface IHealth
    {
        public UnityEvent OnHealthChanged { get; }
        public UnityEvent OnHealed { get; }
        public UnityEvent OnDead { get; }
        
        public void Heal(float amount);
    }
}
