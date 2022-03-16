using UnityEngine;
using SpaceShooter.Actors;
using GamedevsToolbox.ScriptableArchitecture.Values;
using System.Collections.Generic;
using System.Collections;
using SpaceShooter.Obtainables;
using GamedevsToolbox.ScriptableArchitecture.Events;

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

        [SerializeField]
        private ObtainableSpawner obtainableSpawner = default;

        [Header("Configuration")]
        [SerializeField]
        private float wavePointsMultiplier = default;

        [SerializeField]
        private int basePoints = 2000;

        [SerializeField]
        private float timeBetweenEnemies = default;

        [SerializeField]
        [Tooltip("Amount of enemies to kill to spawn an obtainable")]
        private int killsToObtainable = 10;

        [Header("Events")]
        [SerializeField]
        private GameEvent waveFinishedEvent = default;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField]
        private bool showDebugGUI = default;
#endif

        private int totalEnemiesSpawned = 0;
        private int totalEnemiesFinished = 0;
        private bool finishedSpawning = false;
        private int currentPointsUsed = 0;
        private int currentWavePoints = 0;

        private int obtainableKillCounter = 0;

        public int WavePoints => currentWavePoints;
        public int UsedWavePoints => currentPointsUsed;
        public int RemainingWavePoints => WavePoints - UsedWavePoints;
        public int EnemiesSpawned => totalEnemiesSpawned;
        public int EnemiesFinished => totalEnemiesFinished;
        public bool StillSpawning => !finishedSpawning;
        public int EnemiesRemaining => EnemiesSpawned - EnemiesFinished;
        public int ObtainableKillCounter => obtainableKillCounter;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3f);
            NewWave();
        }

        public void NewWave()
        {
            Debug.Log("Starting new wave");
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
            if (CheckWaveFinished())
            {
                FinishWave();
            }
        }

        private void OnEnemyFinished()
        {
            totalEnemiesFinished++;
            obtainableKillCounter++;
            if (CheckWaveFinished())
            {
                FinishWave();
            } else
            {
                // We put in the else because we don't want to spawn an obtainable when changing waves
                if (obtainableKillCounter >= killsToObtainable)
                {
                    obtainableKillCounter = 0;
                    obtainableSpawner.SpawnRandomObtainable();
                }
            }
        }

        private bool CheckWaveFinished()
        {
            return totalEnemiesFinished == totalEnemiesSpawned && finishedSpawning == true;
        }

        private void FinishWave()
        {
            Debug.Log("Finished wave");
            waveFinishedEvent?.Raise();
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

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (!showDebugGUI)
                return;
            GUILayout.Space(200f);
            GUIStyle style = new GUIStyle();
            style.fontSize = 32;
            style.normal.textColor = Color.white;
            style.contentOffset = Vector2.one * 20f;
            GUILayout.Label(string.Format("Wave Points: {0}", WavePoints), style);
            GUILayout.Label(string.Format("Wave Used Points: {0}", UsedWavePoints), style);
            GUILayout.Label(string.Format("Wave Remaining Points: {0}", RemainingWavePoints), style);
            GUILayout.Label(string.Format("Spawned Enemies: {0}", EnemiesSpawned), style);
            GUILayout.Label(string.Format("Enemies Finished: {0}", EnemiesFinished), style);
            GUILayout.Label(string.Format("Enemies Remaining: {0}", EnemiesRemaining), style);
            GUILayout.Label(string.Format("Still Spawning: {0}", StillSpawning), style);
            GUILayout.Label(string.Format("Obtainable Kill Count: {0}", ObtainableKillCounter), style);
        }
#endif
    }
}