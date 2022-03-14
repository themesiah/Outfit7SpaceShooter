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
        private ScriptableIntReference damageReference = default;

        [SerializeField]
        private ScriptableIntReference waveReference = default;

        [SerializeField]
        private float extraDamagePerWave = default;

        [SerializeField]
        private PoolObjectDestroyer poolObjectDestroyer = default;

        [SerializeField]
        private UnityEvent OnCollided = default;

        public int Damage
        {
            get
            {
                int dmg = damageReference.GetValue();
                int wave = waveReference.GetValue();
                int extra = (int)(wave * extraDamagePerWave);
                return dmg + extra;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Not checking for tags because the physics collision matrix already manages colliding with only the necessary objects
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(Damage);
            poolObjectDestroyer.Free();
            OnCollided?.Invoke();
        }
    }
}