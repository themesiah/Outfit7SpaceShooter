using UnityEngine;

namespace SpaceShooter.Scenario
{
    public class MoveTowardsEndZone : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody body = default;

        [SerializeField]
        private float speed = 10f;

        private void Start()
        {
            body.velocity = new Vector3(-speed, 0f, 0f);
        }
    }
}