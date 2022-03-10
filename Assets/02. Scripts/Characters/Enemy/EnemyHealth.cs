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
            health = health + heal;
            if (health > maxHealth)
                health = maxHealth;
        }

        public override void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }
}