using UnityEngine;

namespace SpaceShooter.WeaponsAndBullets
{
    public class BulletForward : MonoBehaviour, IBulletMovement
    {
        [SerializeField]
        private Rigidbody bulletBody = default;

        public Vector3 velocity = default;

        public void StartMovement()
        {
            bulletBody.velocity = transform.worldToLocalMatrix.inverse * velocity;
        }
    }
}