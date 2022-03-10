using UnityEngine;
using SpaceShooter.Actors;
using SpaceShooter.Utils;
using GamedevsToolbox.ScriptableArchitecture.Pools;

namespace SpaceShooter.WeaponsAndBullets
{
    public class BulletCollisionDamage : MonoBehaviour
    {
        [SerializeField]
        private int damage = 10;

        [SerializeField]
        private PoolObjectDestroyer poolObjectDestroyer = default;

        private void OnTriggerEnter(Collider other)
        {
            SimpleLogger.Instance.Log(SimpleLogger.LogContext.Battle, "Collision of bullet {0} with entity {1}", gameObject.name, other.name);
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
            poolObjectDestroyer.Free();
        }
    }
}