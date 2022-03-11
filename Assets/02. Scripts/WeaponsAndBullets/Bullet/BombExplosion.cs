using SpaceShooter.Actors;
using UnityEngine;

namespace SpaceShooter.Utils
{
    public class BombExplosion : MonoBehaviour
    {
        [SerializeField]
        private float radius = default;

        [SerializeField]
        private int damage = 500000;

        [SerializeField]
        private LayerMask layerMask = default;

        public void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            foreach(Collider collider in colliders)
            {
                collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
            }
        }
    }
}