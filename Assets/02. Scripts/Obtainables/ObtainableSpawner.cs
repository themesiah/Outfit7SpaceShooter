using UnityEngine;
using SpaceShooter.WeaponsAndBullets;

namespace SpaceShooter.Obtainables
{
    public class ObtainableSpawner : MonoBehaviour
    {
        [SerializeField]
        private PoolContainer[] obtainablesPools = default;
        [SerializeField]
        private ObtainableRandomSpawnPoint randomPosition = default;

        public void SpawnRandomObtainable()
        {
            var position = randomPosition.GetRandomPosition();
            var pool = obtainablesPools[Random.Range(0, obtainablesPools.Length)];
            pool.pool.GetInstance(instancePosition: position);
        }
    }
}