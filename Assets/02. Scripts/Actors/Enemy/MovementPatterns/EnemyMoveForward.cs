using GamedevsToolbox.ScriptableArchitecture.Values;
using System.Collections;
using UnityEngine;

namespace SpaceShooter.Actors
{
    public class EnemyMoveForward : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Rigidbody enemyBody = default;
        [SerializeField]
        private ScriptableIntReference waveReference = default;
        [Header("Configuration")]
        [SerializeField]
        private float speed = default;
        [SerializeField]
        private bool alwaysUpdateVelocity = false;
        [SerializeField]
        private float extraSpeedPerWave = 0f;

        private float ExtraSpeed => (waveReference.GetValue()-1) * extraSpeedPerWave;

        public void OnEnable()
        {
            // We do this on a coroutine, because enabling takes place at the same time of getting from the pool.
            // At this time, the transform is not yet initialized with the correct "right", so we have to wait a frame for that.
            StartCoroutine(SetVelocityCoroutine());
        }

        private void Update()
        {
            if (alwaysUpdateVelocity)
                SetVelocity();
        }

        private IEnumerator SetVelocityCoroutine()
        {
            yield return null;
            SetVelocity();
        }

        private void SetVelocity()
        {
            enemyBody.velocity = (speed + ExtraSpeed) * transform.right;
        }
    }
}