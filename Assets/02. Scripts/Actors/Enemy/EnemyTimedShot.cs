using UnityEngine;
using SpaceShooter.WeaponsAndBullets;

namespace SpaceShooter.Actors
{
    public class EnemyTimedShot : MonoBehaviour
    {
        [SerializeField]
        private EmitterAbstract[] emitters = default;

        [SerializeField]
        private float burstDelay = 1f;

        float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= burstDelay)
            {
                timer = 0f;
                foreach(var emitter in emitters)
                {
                    emitter.Emit();
                }
            }
        }
    }
}