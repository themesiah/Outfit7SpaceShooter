using GamedevsToolbox.ScriptableArchitecture.Pools;
using UnityEngine;
using static GamedevsToolbox.ScriptableArchitecture.Pools.PoolObjectDestroyer;
using static SpaceShooter.Management.EnemyWaveManager;

namespace SpaceShooter.Actors
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawnerRandomPosition randomPositionProvider = default;

        public void SpawnFormation(EnemyAndFormationPair enemyAndFormation, OnFreedHandler onFreed)
        {
            Vector3[] formationPositions = GetFormationPositions(enemyAndFormation);
            foreach (var position in formationPositions)
            {
                GameObject enemyObject = SpawnEnemy(enemyAndFormation.spawnConfiguration, position);
                // Set up the callbackfor when enemies die or disappear, so we know we finished the wave
                enemyObject.GetComponent<PoolObjectDestroyer>().OnFreed += onFreed;
                // Reset the health component of enemies, so they have full health and are not "already dead" as they spawn
                enemyObject.GetComponent<EnemyHealth>()?.Reset();
            }
        }

        private Vector3[] GetFormationPositions(EnemyAndFormationPair enemyAndFormation)
        {
            // Get size of formation
            Vector2 formationSize = new Vector2(enemyAndFormation.formation.xSeparation * enemyAndFormation.formation.columns - 1,
                                                    enemyAndFormation.formation.ySeparation * enemyAndFormation.formation.rows - 1);
            Vector2 halfFormationSize = formationSize / 2f;
            // Get random position
            Vector2 formationCenter = randomPositionProvider.GetRandomPosition();
            // Define formation positions
            Vector3[] formationPositions = new Vector3[enemyAndFormation.formation.columns * enemyAndFormation.formation.rows];
            for (int i = 0; i < enemyAndFormation.formation.rows; ++i)
            {
                for (int j = 0; j < enemyAndFormation.formation.columns; ++j)
                {
                    formationPositions[j + i * enemyAndFormation.formation.columns] = formationCenter - halfFormationSize + new Vector2(i * enemyAndFormation.formation.xSeparation, j * enemyAndFormation.formation.ySeparation);
                    formationPositions[j + i * enemyAndFormation.formation.columns].z = 10f;
                }
            }

            return formationPositions;
        }

        private GameObject SpawnEnemy(EnemySpawnConfiguration spawnConfiguration, Vector3 position)
        {
            GameObject enemyObject = spawnConfiguration.PoolReference.Get().pool.GetInstance(instancePosition: position);
            var euler = enemyObject.transform.eulerAngles;
            euler.y = 180f;
            enemyObject.transform.eulerAngles = euler;
            return enemyObject;
        }
    }
}