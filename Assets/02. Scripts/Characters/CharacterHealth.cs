using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Actors
{
    public abstract class CharacterHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private UnityEvent OnDie = default;

        [SerializeField]
        private UnityEvent<int> OnReceiveDamage = default;

        [SerializeField]
        private UnityEvent<int> OnHealed = default;

        public void Die()
        {
            OnDie?.Invoke();
        }

        public virtual void Heal(int heal)
        {
            OnHealed?.Invoke(heal);
        }

        public virtual void TakeDamage(int damage)
        {
            OnReceiveDamage?.Invoke(damage);
        }
    }
}