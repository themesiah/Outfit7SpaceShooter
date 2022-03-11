using GamedevsToolbox.Utils;
using UnityEngine;

namespace SpaceShooter.Extensions
{
    public class ObjectPauseManager : MonoBehaviour
    {
        private IPausable[] pausableObjects = default;

        private void Start()
        {
            pausableObjects = GetComponents<IPausable>();
        }

        public void Pause()
        {
            foreach(var pausable in pausableObjects)
            {
                pausable.Pause();
            }
        }

        public void Resume()
        {
            foreach(var pausable in pausableObjects)
            {
                pausable.Resume();
            }
        }
    }
}