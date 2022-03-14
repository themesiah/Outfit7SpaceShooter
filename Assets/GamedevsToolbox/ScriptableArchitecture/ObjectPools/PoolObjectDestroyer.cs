using UnityEngine;
using UnityEngine.Events;

namespace GamedevsToolbox.ScriptableArchitecture.Pools
{
    public class PoolObjectDestroyer : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnFreeObject = default;

        private ScriptablePool pool;

        // Redundant with "OnFreeObject", but helps the developer differentiate between editor assigned events and programatically assigned events
        public delegate void OnFreedHandler();
        public event OnFreedHandler OnFreed;

        public void InitWithPool(ScriptablePool pool)
        {
            this.pool = pool;
        }

        public void Free()
        {
            // We check if it is active to not trigger an unity error in case we try to "free" it twice in a frame
            if (gameObject.activeInHierarchy)
            {
                OnFreed?.Invoke();
                OnFreeObject?.Invoke();
                OnFreed = null;
                if (pool != null)
                {
                    pool.FreeInstance(gameObject);
                } else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}