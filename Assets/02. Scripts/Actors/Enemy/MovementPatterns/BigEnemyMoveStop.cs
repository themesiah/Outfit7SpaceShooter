using GamedevsToolbox.ScriptableArchitecture.Sets;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Actors
{
    public class BigEnemyMoveStop : MonoBehaviour
    {
        private static float STOP_THRESHOLD = 0.3f;

        [Header("References")]
        [SerializeField]
        private Rigidbody enemyBody = default;
        [SerializeField]
        private RuntimeSingleTransform stopPointReference = default;
        [Header("Configuration")]
        [SerializeField]
        private float movementSpeed = 10f;
        [SerializeField]
        private float movementSpeedAfterStop = 10f;
        [SerializeField]
        private float timeStopped = 5f;
        [Header("Events")]
        [SerializeField]
        private UnityEvent OnStopped = default;
        [SerializeField]
        private UnityEvent OnResumed = default;

        private void Start()
        {
            StartCoroutine(Movement());
        }

        private IEnumerator Movement()
        {
            enemyBody.velocity = transform.right * movementSpeed;
            float distance = Mathf.Abs(stopPointReference.Get().position.x - transform.position.x);
            // Wait until we are near the stop point
            while (distance > STOP_THRESHOLD)
            {
                distance = Mathf.Abs(stopPointReference.Get().position.x - transform.position.x);
                yield return null;
            }
            // Stop the enemy
            enemyBody.velocity = Vector3.zero;
            OnStopped?.Invoke();
            // Wait a few seconds (the enemy should be shoting in this time)
            yield return new WaitForSeconds(timeStopped);
            OnResumed?.Invoke();
            // Resume movement with different speed (faster exit?)
            enemyBody.velocity = transform.right * movementSpeedAfterStop;
        }
    }
}