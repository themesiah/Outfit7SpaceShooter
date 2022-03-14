using GamedevsToolbox.ScriptableArchitecture.Sets;
using GamedevsToolbox.ScriptableArchitecture.Values;
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
        [SerializeField]
        private ScriptableIntReference waveReference = default;
        [Header("Configuration")]
        [SerializeField]
        private float movementSpeed = 10f;
        [SerializeField]
        private float movementSpeedAfterStop = 10f;
        [SerializeField]
        private float timeStopped = 5f;
        [SerializeField]
        private float extraSpeedPerWave = 0f;
        [Header("Events")]
        [SerializeField]
        private UnityEvent OnStopped = default;
        [SerializeField]
        private UnityEvent OnResumed = default;

        private Coroutine movementCoroutine;

        private float ExtraSpeed => (waveReference.GetValue()-1) * extraSpeedPerWave;

        private void OnEnable()
        {
            movementCoroutine = StartCoroutine(Movement());
        }

        private void OnDisable()
        {
            StopCoroutine(movementCoroutine);
        }

        private IEnumerator Movement()
        {
            yield return null;
            enemyBody.velocity = (movementSpeed + ExtraSpeed) * transform.right;
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
            enemyBody.velocity = (movementSpeedAfterStop + ExtraSpeed) * transform.right;
        }
    }
}