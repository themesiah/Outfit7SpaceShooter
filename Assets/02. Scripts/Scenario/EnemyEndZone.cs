using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Pools;

namespace SpaceShooter.Scenario
{
    public class EnemyEndZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PoolObjectDestroyer>()?.Free();
        }
    }
}