using System.Collections;
using UnityEngine;

namespace SpaceShooter.Scenario
{
    public class BackEnemiesMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform xStart = default;

        [SerializeField]
        private Transform xEnd = default;

        [SerializeField]
        private Vector2 minMaxSpeed = default;

        [SerializeField]
        private Vector2 minMaxWaitToStart = default;

        private void Start()
        {
            StartCoroutine(MovementCoroutine());
        }

        private IEnumerator MovementCoroutine()
        {
            float positionXStart = xStart.position.x;
            float positionXEnd = xEnd.position.x;

            while(true)
            {
                yield return new WaitForSeconds(Random.Range(minMaxWaitToStart.x, minMaxWaitToStart.y));

                var currentPosition = transform.position;
                currentPosition.x = positionXStart;
                transform.position = currentPosition;
                var targetPosition = currentPosition;
                targetPosition.x = positionXEnd;
                float speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);

                while (targetPosition.x - transform.position.x > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }
}