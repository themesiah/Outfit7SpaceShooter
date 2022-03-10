using UnityEngine;

namespace SpaceShooter.WeaponsAndBullets
{
    public class BulletForward : MonoBehaviour, IBulletMovement
    {
        [SerializeField]
        private Rigidbody bulletBody = default;
        [SerializeField]
        private float speed = default;

        public void StartMovement()
        {
            bulletBody.velocity = transform.right * speed;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}