using UnityEngine;
using UnityEngine.Events;
using SpaceShooter.WeaponsAndBullets;

namespace SpaceShooter.Actors
{
    public class EnemyTimedShot : MonoBehaviour
    {
        [SerializeField]
        private EmitterAbstract[] emitters = default;

        [SerializeField]
        private float burstDelay = 1f;

        [SerializeField]
        private UnityEvent OnEmitted = default;

        float timer = 0f;

        private void OnDisable()
        {
            timer = 0f;
        }

        private void Update()
        {
            if (burstDelay <= 0f)
                return;
            timer += Time.deltaTime;
            if (timer >= burstDelay)
            {
                timer = 0f;
                Emit();
            }
        }

        public void Emit()
        {
            foreach (var emitter in emitters)
            {
                emitter.Emit();
            }
            OnEmitted?.Invoke();
        }
    }
}