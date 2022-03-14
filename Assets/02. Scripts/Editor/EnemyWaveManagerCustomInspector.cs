using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SpaceShooter.Management;

namespace SpaceShooter.EditorScripts
{
    [CustomEditor(typeof(EnemyWaveManager))]
    public class EnemyWaveManagerCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (EditorApplication.isPlaying)
            {
                EnemyWaveManager ewm = ((EnemyWaveManager)target);
                EditorGUILayout.LabelField("Wave Points", ewm.WavePoints.ToString());
                EditorGUILayout.LabelField("Used Wave Points", ewm.UsedWavePoints.ToString());
                EditorGUILayout.LabelField("Remaining Wave Points", ewm.RemainingWavePoints.ToString());
                EditorGUILayout.LabelField("Total Enemies Spawned", ewm.EnemiesSpawned.ToString());
                EditorGUILayout.LabelField("Total Enemies Finished", ewm.EnemiesFinished.ToString());
                EditorGUILayout.LabelField("Total Enemies Remaining", ewm.EnemiesRemaining.ToString());
                EditorGUILayout.LabelField("Still Spawning", ewm.StillSpawning.ToString());
                EditorGUILayout.LabelField("Obtainable Kill Counter", ewm.ObtainableKillCounter.ToString());
            }
        }
    }
}