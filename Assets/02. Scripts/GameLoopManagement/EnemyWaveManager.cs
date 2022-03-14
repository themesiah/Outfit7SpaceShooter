using UnityEngine;
using SpaceShooter.Actors;
using GamedevsToolbox.ScriptableArchitecture.Values;
using System.Collections.Generic;
using System.Collections;
using GamedevsToolbox.ScriptableArchitecture.Pools;

namespace SpaceShooter.Management
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public struct EnemyAndFormationPair
        {
            public EnemySpawnConfiguration spawnConfiguration;
            public int formationIndex;
            public EnemySpawnConfiguration.SpawnFormation formation;
        }

        [Header("References")]
        [SerializeField]
        private EnemySpawnConfiguration[] spawnableEnemies = default;

        [SerializeField]
        private EnemySpawner enemySpawner = default;

        [SerializeField]
        private ScriptableIntReference currentWaveReference = default;

        [Header("Configuration")]
        [SerializeField]
        private float wavePointsMultiplier = default;

        [SerializeField]
        private int basePoints = 2000;

        [SerializeField]
        private float timeBetweenEnemies = default;

        private int totalEnemiesSpawned = 0;
        private int totalEnemiesFinished = 0;
        private bool finishedSpawning = false;
        private int currentPointsUsed = 0;
        private int currentWavePoints = 0;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            NewWave();
        }

        public void NewWave()
        {
            currentWaveReference.SetValue(currentWaveReference.GetValue() + 1);
            totalEnemiesSpawned = 0;
            totalEnemiesFinished = 0;
            finishedSpawning = false;
            currentPointsUsed = 0;
            float multiplier = 1f + ((currentWaveReference.GetValue() - 1f) * wavePointsMultiplier);
            currentWavePoints = (int) (multiplier * basePoints);
            StartCoroutine(WaveCoroutine());
        }

        private IEnumerator WaveCoroutine()
        {
            EnemyAndFormationPair enemyAndFormation = GetValidEnemy(currentWavePoints - currentPointsUsed);
            while (enemyAndFormation.spawnConfiguration != null)
            {
                enemySpawner.SpawnFormation(enemyAndFormation, OnEnemyFinished);

                // Update points and amount of enemies
                currentPointsUsed += enemyAndFormation.formation.GetPoints(enemyAndFormation.spawnConfiguration.Points);
                totalEnemiesSpawned += enemyAndFormation.formation.rows * enemyAndFormation.formation.columns;
                Debug.LogFormat("Current points used {0}", currentPointsUsed);
                Debug.LogFormat("Total enemies spawned {0}", totalEnemiesSpawned);

                yield return new WaitForSeconds(timeBetweenEnemies);
                enemyAndFormation = GetValidEnemy(currentWavePoints - currentPointsUsed);
            }
            finishedSpawning = true;

        }

        private void OnEnemyFinished()
        {
            totalEnemiesFinished++;
            if (totalEnemiesFinished == totalEnemiesSpawned && finishedSpawning == true)
            {
                NewWave();
            }
        }

        private EnemyAndFormationPair GetValidEnemy(int maxPoints)
        {
            // We first prepare a list of enemy candidates.
            // A candidate is an enemy with a formation with a total cost less than the points remaining for the wave
            List<EnemySpawnConfiguration> candidateList = new List<EnemySpawnConfiguration>();
            List<int> formationIndexes = new List<int>();
            foreach(var spawnable in spawnableEnemies)
            {
                for (int i = 0; i < spawnable.SpawnFormations.Length; ++i)
                {
                    int cost = spawnable.SpawnFormations[i].GetPoints(spawnable.Points);
                    if (cost <= maxPoints)
                    {
                        for (int j = 0; j < spawnable.Weight; ++j)
                        {
                            // A candidate will be on the list "spawnable.Weight" times in the list per formation. This way it have more chance to appear (that's what's the weight is for)
                            candidateList.Add(spawnable);
                            formationIndexes.Add(i);
                        }
                    }
                }
            }

            // Empty object if there are no candidates (not enough points, wave finished)
            if (candidateList.Count == 0)
            {
                return new EnemyAndFormationPair();
            }

            // We get a random candidate from the list. The ones that appear most times are easier to appear randomly
            int index = Random.Range(0, candidateList.Count);
            return new EnemyAndFormationPair { spawnConfiguration = candidateList[index], formationIndex = formationIndexes[index], formation = candidateList[index].SpawnFormations[formationIndexes[index]] };
        }
    }
}