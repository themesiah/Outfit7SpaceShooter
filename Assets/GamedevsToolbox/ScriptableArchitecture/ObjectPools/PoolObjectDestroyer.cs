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
            // We check if it is active to not trigger an unity error in case we try to "free" it twice in a frame
            if (gameObject.activeInHierarchy)
            {
                pool.FreeInstance(gameObject);
            }
        }
    }
}