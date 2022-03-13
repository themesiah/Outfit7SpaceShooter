using GamedevsToolbox.ScriptableArchitecture.Events;
using UnityEngine;

namespace SpaceShooter.Actors
{
    public class EnemyPointsObtainer : MonoBehaviour
    {
        [SerializeField]
        private IntGameEvent addScoreEvent = default;
        [SerializeField]
        private int points = default;

        public int Points => points;

        public void GetPoints()
        {
            addScoreEvent?.Raise(points);
        }
    }
}