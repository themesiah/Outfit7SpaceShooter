using UnityEngine;

namespace GamedevsToolbox.ScriptableArchitecture.Pools
{
    public class PoolObjectDestroyer : MonoBehaviour
    {
        private ScriptablePool pool;

        public void InitWithPool(ScriptablePool pool)
        {
            this.pool = pool;
        }

        public void Free()
        {
            pool.FreeInstance(gameObject);
        }
    }
}