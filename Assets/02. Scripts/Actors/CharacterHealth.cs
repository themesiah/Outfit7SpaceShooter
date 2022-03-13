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

        private bool alreadyDied = false;

        public void Die()
        {
            // We want to control the case in which a enemy is hit by two bullets at the same time and dies from both. It should only die once
            if (!alreadyDied)
            {
                alreadyDied = true;
                OnDie?.Invoke();
            }
        }

        public virtual void Heal(int heal)
        {
            OnHealed?.Invoke(heal);
        }

        public virtual void TakeDamage(int damage)
        {
            OnReceiveDamage?.Invoke(damage);
        }

        public virtual void Reset()
        {
            alreadyDied = false;
        }
    }
}