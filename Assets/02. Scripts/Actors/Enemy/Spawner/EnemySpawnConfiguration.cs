using SpaceShooter.Extensions;
using UnityEngine;

namespace SpaceShooter.Actors
{
    [CreateAssetMenu(menuName = "Space Shoter/Enemy Spawn Configuration")]
    public class EnemySpawnConfiguration : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Name of the enemy on the spawner object")]
        private string spawnableName = default;

        [SerializeField]
        [Tooltip("Manages the chances of it getting spawned. We want mroe enemies than obstacles")]
        private int spawnWeight = default;

        [SerializeField]
        [Tooltip("Waves have an internal points system, so more difficult enemies have more points and then appear less")]
        private int points = default;

        [SerializeField]
        private RuntimeSingleBulletPoolContainer poolReference = default;

        [System.Serializable]
        public struct SpawnFormation
        {
            public int rows;
            public int columns;
            public float xSeparation;
            public float ySeparation;
            public int GetPoints(int pointsSingle)
            {
                return pointsSingle * rows * columns;
            }
        }

        [SerializeField]
        [Tooltip("In which ways can the enemy be spawned? Any number greater than 1 in columns or rows result in multiple enemies spawned in a rectangle formation")]
        private SpawnFormation[] spawnConfigurations = default;

        public string SpawnableName => spawnableName;
        public SpawnFormation[] SpawnFormations => spawnConfigurations;
        public RuntimeSingleBulletPoolContainer PoolReference => poolReference;
        public int Points => points;
        public int Weight => spawnWeight;
    }
}