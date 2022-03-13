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
                Debug.Log("Game resumed");
                resumeEvent?.Raise();
                Time.timeScale = 1f;
            }
            else
            {
                Debug.Log("Game paused");
                pauseEvent?.Raise();
                Time.timeScale = 0f;
            }
            currentlyPaused = !currentlyPaused;
        }
    }
}