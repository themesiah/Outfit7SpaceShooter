using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Pools;

namespace SpaceShooter.Scenario
{
    public class EnemyEndZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Not checking for tags because the physics collision matrix already manages colliding with only the necessary objects
            PoolObjectDestroyer poolDestroyer = other.GetComponent<PoolObjectDestroyer>();
            if (poolDestroyer != null)
            {
                poolDestroyer.Free();
            } else
            {
                Destroy(other.gameObject);
            }
        }
    }
}