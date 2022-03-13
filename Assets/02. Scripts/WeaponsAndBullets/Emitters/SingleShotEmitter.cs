using UnityEngine;

namespace SpaceShooter.WeaponsAndBullets
{
    public class SingleShotEmitter : EmitterAbstract
    {
        [SerializeField]
        private Transform spawnPoint = default;
        [SerializeField]
        private PoolContainer bulletContainer = default;

        public override void Emit()
        {
            GameObject go = bulletContainer.pool.GetInstance(null, spawnPoint.position, spawnPoint.rotation);
            go.GetComponent<IBulletMovement>()?.StartMovement();
        }
    }
}