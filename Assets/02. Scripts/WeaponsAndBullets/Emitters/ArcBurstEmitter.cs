using UnityEngine;
using SpaceShooter.Extensions;

namespace SpaceShooter.WeaponsAndBullets
{
    public class ArcBurstEmitter : EmitterAbstract
    {
        [Header("References")]
        [SerializeField]
        private Transform spawnPoint = default;
        [SerializeField]
        private RuntimeSingleBulletPoolContainer bulletContainerReference = default;

        [Header("Arc configuration")]
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

        private void Awake()
        {
            tempEuler = spawnPoint.rotation.eulerAngles;

            angleStart = tempEuler.z - arcAngle / 2f;
            angleStep = arcAngle / (arcRows - 1);
        }

        public override void Emit()
        {
            for (int i = 0; i < bulletsPerStack; ++i)
            {
                for (int j = 0; j < arcRows; ++j)
                {
                    tempEuler.z = angleStart + j * angleStep;
                    Quaternion quat = Quaternion.Euler(tempEuler);
                    GameObject go = bulletContainerReference.Get().pool.GetInstance(null, spawnPoint.position, quat);
                    BulletForward bulletForward = go.GetComponent<BulletForward>();
                    bulletForward.velocity = Vector3.right * Mathf.Lerp(minMaxStackSpeed.x, minMaxStackSpeed.y, (float)((float)i / (float)bulletsPerStack));
                    bulletForward.StartMovement();
                }
            }
        }
    }
}