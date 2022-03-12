using UnityEngine;
using SpaceShooter.Extensions;
using GamedevsToolbox.ScriptableArchitecture.Pools;

namespace SpaceShooter.WeaponsAndBullets
{
    // BulletPoolContainers just contains a single pool object, so multiple emitters can get access to the same pool.
    public class BulletPoolContainer : MonoBehaviour
    {
        public AddressableScriptablePool pool;

        private void Awake()
        {
            pool.InitPool();
        }
    }
}