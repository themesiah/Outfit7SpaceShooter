using UnityEngine;
using UnityEngine.Events;
using SpaceShooter.Extensions;
using System.Collections;
using GamedevsToolbox.ScriptableArchitecture.Sets;

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
        [SerializeField]
        private RuntimeSingleTransform playerTransformReference = default;

        [Header("Arc configuration")]
        [SerializeField]
        [Tooltip("Time before the bursts start. Useful for desynchronizing multiple ArcBurstEmitter emitting at the same time")]
        private float startDelay = 0f;

        [SerializeField]
        [Tooltip("Sets if the center of the arc is aimed to the player")]
        private bool facePlayer = false;

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

        [SerializeField]
        [Tooltip("The resulting will have this number added. Useful for using multiple arc burst emitters and doing nice patterns")]
        private float angleOffset = 0f;

        [Header("Events")]
        [SerializeField]
        private UnityEvent OnBurst = default;

        private float angleStart;
        private float angleStep;
        private Vector3 tempEuler;
        private Coroutine emisionCoroutine = null;

        private void PrecalculateArcAngles()
        {
            tempEuler = spawnPoint.rotation.eulerAngles;

            angleStart = tempEuler.z - arcAngle / 2f;
            angleStep = arcRows == 1 ? 0f : (arcAngle / (arcRows - 1));
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
            PrecalculateArcAngles();
            yield return new WaitForSeconds(startDelay);
            float playerFacingAngle = 0f;
            if (facePlayer && playerTransformReference.Get() != null)
            {
                Vector3 direction = playerTransformReference.Get().position - spawnPoint.position;
                playerFacingAngle = Vector3.Angle(spawnPoint.right, direction);
                if (spawnPoint.position.y > playerTransformReference.Get().position.y)
                    playerFacingAngle *= -1f;
            }

            for (int w = 0; w < numberOfBursts; ++w)
            {
                OnBurst?.Invoke();
                for (int i = 0; i < bulletsPerStack; ++i)
                {
                    for (int j = 0; j < arcRows; ++j)
                    {
                        Quaternion quat = GetAngle(playerFacingAngle, j);
                        GenerateBullet(i, quat);
                    }
                }
                // Wait for next burst
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }

        private void GenerateBullet(int i, Quaternion quat)
        {
            GameObject go = bulletContainerReference.Get().pool.GetInstance(null, spawnPoint.position, quat);
            // We set a new speed for the bullet to make stacks (depends on it position on the stack) and initialize its movement
            IBulletMovement bulletForward = go.GetComponent<IBulletMovement>();
            if (bulletsPerStack > 1)
            {
                bulletForward?.SetSpeed(Mathf.Lerp(minMaxStackSpeed.x, minMaxStackSpeed.y, (float)((float)i / (float)bulletsPerStack)));
            }
            else
            {
                bulletForward?.SetSpeed(minMaxStackSpeed.y);
            }
            bulletForward?.StartMovement();
        }

        private Quaternion GetAngle(float playerFacingAngle, int j)
        {
            // We calculate the angle of this row and get the bullet from the pool facing that direction
            tempEuler.z = angleStart + j * angleStep;

            if (facePlayer && playerTransformReference.Get() != null)
            {
                tempEuler.z += playerFacingAngle;
            }

            tempEuler.z += angleOffset;

            Quaternion quat = Quaternion.Euler(tempEuler);
            return quat;
        }
    }
}