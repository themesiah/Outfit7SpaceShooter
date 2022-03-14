using GamedevsToolbox.ScriptableArchitecture.Values;
using UnityEngine;

namespace SpaceShooter.Actors
{
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField]
        private ScriptableIntReference waveReference = default;

        [SerializeField]
        private int maxHealth = 100;

        [SerializeField]
        private int extraHealthPerWave = 20;

        private int health = 0;

        private int MaxHealth {
            get {
                int extra = extraHealthPerWave * (waveReference.GetValue() - 1);
                return maxHealth + extra;
            }
        }

        private void Start()
        {
            Reset();
        }

        public override void Heal(int heal)
        {
            base.Heal(heal);
            health = health + heal;
            if (health > MaxHealth)
                health = MaxHealth;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public override void Reset()
        {
            base.Reset();
            health = MaxHealth;
        }
    }
}