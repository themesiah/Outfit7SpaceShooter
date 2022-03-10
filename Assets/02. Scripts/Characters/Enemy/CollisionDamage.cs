using GamedevsToolbox.ScriptableArchitecture.Values;
using UnityEngine;

namespace SpaceShooter.Actors
{
    public class CollisionDamage : MonoBehaviour
    {
        [SerializeField]
        private ScriptableIntReference collisionDamageReference = default;

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IDamageable>()?.TakeDamage(collisionDamageReference.GetValue());
        }
    }
}
