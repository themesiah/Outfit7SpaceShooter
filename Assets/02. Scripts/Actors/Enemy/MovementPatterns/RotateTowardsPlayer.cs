using GamedevsToolbox.ScriptableArchitecture.Sets;
using UnityEngine;

namespace SpaceShooter.Actors
{
    public class RotateTowardsPlayer : MonoBehaviour
    {
        [SerializeField]
        private RuntimeSingleTransform playerTransformReference = default;

        [SerializeField]
        private float rotationSpeed = 50f;

        [SerializeField]
        private float maxAngle = 45f;

        private void Update()
        {
            if (playerTransformReference.Get() != null)
            {
                // Direction is the vector between enemy and player
                Vector3 direction = (playerTransformReference.Get().position - transform.position).normalized;

                float angle = Vector3.Angle(transform.right, direction);

                if (angle < maxAngle)
                {
                    // If we set the right direction as the direction between enemy and player, the enemy will move towards the player
                    transform.right = Vector3.RotateTowards(transform.right, direction, rotationSpeed * Time.deltaTime * Mathf.Deg2Rad, 0f);
                }
            }
        }
    }
}