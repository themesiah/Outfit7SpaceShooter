using GamedevsToolbox.Utils;
using UnityEngine;

namespace SpaceShooter.Extensions
{
    public abstract class PausableObject : MonoBehaviour, IPausable
    {
        protected bool paused = false;

        public virtual void Pause()
        {
            paused = true;
        }

        public virtual void Resume()
        {
            paused = false;
        }
    }
}