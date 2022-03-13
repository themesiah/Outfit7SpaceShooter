using UnityEngine;

namespace SpaceShooter.Actors
{
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField]
        private int maxHealth = 100;

        private int health = 100;

        public override void Heal(int heal)
        {
            base.Heal(heal);
            health = health + heal;
            if (health > maxHealth)
                health = maxHealth;
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
            health = maxHealth;
        }
    }
}