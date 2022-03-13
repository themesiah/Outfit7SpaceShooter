using UnityEngine;
using SpaceShooter.Extensions;

namespace SpaceShooter.WeaponsAndBullets
{
    // PoolContainers just contains a single pool object, so multiple sources can get access to the same pool.
    public class PoolContainer : MonoBehaviour
    {
        public AddressableScriptablePool pool;

        private void Awake()
        {
            pool.InitPool();
        }

        private void OnDestroy()
        {
            pool.FreeAll();
        }
    }
}