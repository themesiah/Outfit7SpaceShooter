namespace SpaceShooter.Actors
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void Heal(int heal);
        void Die();
    }
}