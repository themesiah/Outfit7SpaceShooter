using UnityEngine;

namespace SpaceShooter.Actors
{
    public class EnemyMoveForward : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody enemyBody = default;
        [SerializeField]
        private float speed = default;
        [SerializeField]
        private bool alwaysUpdateVelocity = false;

        public void Start()
        {
            SetVelocity();
        }

        private void Update()
        {
            if (alwaysUpdateVelocity)
                SetVelocity();
        }

        private void SetVelocity()
        {
            enemyBody.velocity = transform.right * speed;
        }
    }
}