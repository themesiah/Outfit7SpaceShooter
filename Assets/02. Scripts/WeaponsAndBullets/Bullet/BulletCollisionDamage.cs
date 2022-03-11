using UnityEngine;
using UnityEngine.Events;
using SpaceShooter.Actors;
using SpaceShooter.Utils;
using GamedevsToolbox.ScriptableArchitecture.Pools;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace SpaceShooter.WeaponsAndBullets
{
    public class BulletCollisionDamage : MonoBehaviour
    {
        [SerializeField]
        private ScriptableIntReference damage = default;

        [SerializeField]
        private PoolObjectDestroyer poolObjectDestroyer = default;

        [SerializeField]
        private UnityEvent OnCollided = default;

        private void OnTriggerEnter(Collider other)
        {
            // Not checking for tags because the physics collision matrix already manages colliding with only the necessary objects
            SimpleLogger.Instance.Log(SimpleLogger.LogContext.Battle, "Collision of bullet {0} with entity {1}", gameObject.name, other.name);
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage.GetValue());
            poolObjectDestroyer.Free();
            OnCollided?.Invoke();
        }
    }
}