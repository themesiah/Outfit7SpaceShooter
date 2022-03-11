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
            // Not checking for tags because the physics collision matrix already manages colliding with only the necessary objects
            other.GetComponent<IDamageable>()?.TakeDamage(collisionDamageReference.GetValue());
        }
    }
}
