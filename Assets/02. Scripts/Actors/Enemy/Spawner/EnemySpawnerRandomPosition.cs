using SpaceShooter.Scenario;
using UnityEngine;

namespace SpaceShooter.Actors
{
    public class EnemySpawnerRandomPosition : MonoBehaviour, IRandomPositionObtainer
    {
        [SerializeField]
        private Transform[] spawnPoints = default;

        public Vector3 GetRandomPosition()
        {
            return spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }
    }
}