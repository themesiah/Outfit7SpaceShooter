using GamedevsToolbox.ScriptableArchitecture.Values;
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
            enemyBody.velocity = (speed + ExtraSpeed) * transform.right;
        }
    }
}