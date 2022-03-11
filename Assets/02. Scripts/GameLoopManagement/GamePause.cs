using GamedevsToolbox.ScriptableArchitecture.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceShooter.Utils;

namespace SpaceShooter.Management
{
    public class GamePause : MonoBehaviour
    {
        [SerializeField]
        private GameEvent pauseEvent = default;
        [SerializeField]
        private GameEvent resumeEvent = default;

        private bool currentlyPaused = false;

        public void Pause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Pause();
            }
        }

        public void Pause()
        {
            if (currentlyPaused)
            {
                SimpleLogger.Instance.Log(SimpleLogger.LogContext.UI, "Game resumed");
                resumeEvent?.Raise();
                Time.timeScale = 1f;
            }
            else
            {
                SimpleLogger.Instance.Log(SimpleLogger.LogContext.UI, "Game paused");
                pauseEvent?.Raise();
                Time.timeScale = 0f;
            }
            currentlyPaused = !currentlyPaused;
        }
    }
}