using UnityEngine;
using SpaceShooter.Extensions;
using System.Collections;

namespace SpaceShooter.WeaponsAndBullets
{
    // With this class we can emit a lot of linear patterns
    // (that means any pattern with bullets that don't change directions)
    // This include rings, arcs, stacks and any combination of those
    public class ArcBurstEmitter : EmitterAbstract
    {
        [Header("References")]
        [SerializeField]
        private Transform spawnPoint = default;
        [SerializeField]
        private RuntimeSingleBulletPoolContainer bulletContainerReference = default;

        [Header("Arc configuration")]
        [SerializeField]
        [Tooltip("Number of times the same pattern will be shot")]
        private int numberOfBursts = 1;

        [SerializeField]
        [Tooltip("Time in seconds between bursts")]
        private float timeBetweenBursts = 0.3f;

        [SerializeField]
        [Tooltip("Number of bullets in the same row. They will have different speed.")]
        private int bulletsPerStack = 3;

        [SerializeField]
        [Tooltip("Speed of stacked bullets. The first bullet always have the min speed, and the last one the max speed")]
        private Vector2 minMaxStackSpeed = default;

        [SerializeField]
        [Tooltip("Number of rows the arc will have")]
        private int arcRows = 3;

        [SerializeField]
        [Tooltip("Angle the arc will draw from the origin of spawnpoint. A 360 arc is a ring. A 0 arc is a line")]
        [Range(0f, 360f)]
        private float arcAngle = 45f;

        private float angleStart;
        private float angleStep;
        private Vector3 tempEuler;
        private Coroutine emisionCoroutine = null;

        private void Awake()
        {
            PrecalculateArcAngles();
        }

        private void PrecalculateArcAngles()
        {
            tempEuler = spawnPoint.rotation.eulerAngles;

            angleStart = tempEuler.z - arcAngle / 2f;
            angleStep = arcAngle / (arcRows - 1);
        }

        // We use a coroutine to make use of WaitForSeconds, to use multiple bursts
        public override void Emit()
        {
            if (emisionCoroutine != null)
                StopCoroutine(emisionCoroutine);
            emisionCoroutine = StartCoroutine(EmitCoroutine());
        }

        private IEnumerator EmitCoroutine()
        {
            for (int w = 0; w < numberOfBursts; ++w)
            {
                for (int i = 0; i < bulletsPerStack; ++i)
                {
                    for (int j = 0; j < arcRows; ++j)
                    {
                        // We calculate the angle of this row and get the bullet from the pool facing that direction
                        tempEuler.z = angleStart + j * angleStep;
                        Quaternion quat = Quaternion.Euler(tempEuler);
                        GameObject go = bulletContainerReference.Get().pool.GetInstance(null, spawnPoint.position, quat);
                        // We set a new speed for the bullet to make stacks (depends on it position on the stack) and initialize its movement
                        IBulletMovement bulletForward = go.GetComponent<IBulletMovement>();
                        bulletForward?.SetSpeed(Mathf.Lerp(minMaxStackSpeed.x, minMaxStackSpeed.y, (float)((float)i / (float)bulletsPerStack)));
                        bulletForward?.StartMovement();
                    }
                }
                // Wait for next burst
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }
    }
}