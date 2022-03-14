using SpaceShooter.Scenario;
using UnityEngine;

namespace SpaceShooter.Obtainables
{
    public class ObtainableRandomSpawnPoint : MonoBehaviour, IRandomPositionObtainer
    {
        [SerializeField]
        private Transform minTransformPosition = default;
        [SerializeField]
        private Transform maxTransformPosition = default;

        public Vector3 GetRandomPosition()
        {
            return Vector3.Lerp(minTransformPosition.position, maxTransformPosition.position, Random.value);
        }
    }
}