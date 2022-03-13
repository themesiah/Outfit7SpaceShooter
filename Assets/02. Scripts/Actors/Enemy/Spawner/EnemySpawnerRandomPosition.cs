using UnityEngine;

namespace SpaceShooter.Actors
{
    public class EnemySpawnerRandomPosition : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPoints = default;

        public Vector3 GetRandomPosition()
        {
            return spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }
    }
}