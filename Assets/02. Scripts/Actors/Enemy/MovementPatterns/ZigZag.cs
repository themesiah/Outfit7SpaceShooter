using GamedevsToolbox.ScriptableArchitecture.Sets;
using GamedevsToolbox.ScriptableArchitecture.Values;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Actors
{
    public class ZigZag : MonoBehaviour
    {
        private static float STOP_THRESHOLD = 0.4f;

        [Header("References")]
        [SerializeField]
        private Rigidbody enemyBody = default;

        [SerializeField]
        private ScriptableIntReference waveReference = default;

        [Header("Configuration")]
        [SerializeField]
        private float speed = 10f;

        [SerializeField]
        private float extraSpeedPerWave = 2f;

        [SerializeField]
        private RuntimeSingleTransform maxYReference = default;

        [SerializeField]
        private RuntimeSingleTransform minYReference = default;

        [SerializeField]
        private int numberOfZigZags = default;

        [SerializeField]
        [Tooltip("Set it to a negative number to go from right to left")]
        private float xDistanceBetweenZigZags = default;

        [SerializeField]
        [Tooltip("Time when the enemy is stopped after moving from point to point before resuming movement")]
        private float stopTimeBetweenZigZags = default;

        [Header("Events")]
        [SerializeField]
        private UnityEvent OnStopZigZag = default;

        [SerializeField]
        private UnityEvent onResumeZigZag = default;

        private float ExtraSpeed => extraSpeedPerWave * (waveReference.GetValue()-1);

        private Coroutine zigZagCoroutine;
        private bool nextIsMaxY = false;

        private void OnEnable()
        {
            zigZagCoroutine = StartCoroutine(ZigZagCoroutine());
        }

        private void OnDisable()
        {
            StopCoroutine(zigZagCoroutine);
        }

        private IEnumerator ZigZagCoroutine()
        {
            yield return null;
            if (Random.value < 0.5f)
                nextIsMaxY = !nextIsMaxY;
            onResumeZigZag?.Invoke();
            int zigZagsDone = 0;
            while (zigZagsDone < numberOfZigZags) {
                float targetYposition = GetRandomYPosition();
                Debug.Log(targetYposition);
                Vector3 targetPoint = transform.position + Vector3.right * xDistanceBetweenZigZags;
                Debug.Log(targetPoint.x);
                targetPoint.y = targetYposition;
                Debug.Log(targetPoint);
                Vector3 direction = (targetPoint - transform.position).normalized;
                Debug.Log(direction);
                enemyBody.velocity = (speed + ExtraSpeed) * direction;
                while (STOP_THRESHOLD < Mathf.Abs(transform.position.x-targetPoint.x))
                {
                    yield return null;
                }
                OnStopZigZag?.Invoke();
                enemyBody.velocity = Vector3.zero;
                yield return new WaitForSeconds(stopTimeBetweenZigZags);
                onResumeZigZag?.Invoke();
                nextIsMaxY = !nextIsMaxY;
                zigZagsDone++;
            }
            OnStopZigZag?.Invoke();
            enemyBody.velocity = (speed + ExtraSpeed) * transform.right;
        }

        private float GetRandomYPosition()
        {
            float targetY = 0f;
            if (nextIsMaxY)
            {
                targetY = maxYReference.Get().position.y;
            } else
            {
                targetY = minYReference.Get().position.y;
            }

            // We get a random y between ourselves and the target. We do a square root to make the random value bigger (as we prefer a bigger movement)
            return Mathf.Lerp(transform.position.y, targetY, Mathf.Sqrt(Random.value));
        }
    }
}