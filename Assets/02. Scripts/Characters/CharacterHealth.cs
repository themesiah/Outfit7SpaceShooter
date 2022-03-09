using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Actors
{
    public abstract class CharacterHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private UnityEvent OnDie = default;

        public void Die()
        {
            OnDie?.Invoke();
        }

        public abstract void Heal(int heal);

        public abstract void TakeDamage(int damage);
    }
}