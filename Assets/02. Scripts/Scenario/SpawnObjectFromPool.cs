using SpaceShooter.Extensions;
using UnityEngine;

namespace SpaceShooter.Scenario
{
    public class SpawnObjectFromPool : MonoBehaviour
    {
        [SerializeField]
        private RuntimeSingleBulletPoolContainer poolReference = default;

        [SerializeField]
        private Vector3 offset = default;

        public void Spawn()
        {
            poolReference.Get().pool.GetInstance(instancePosition: transform.position + offset);
        }
    }
}