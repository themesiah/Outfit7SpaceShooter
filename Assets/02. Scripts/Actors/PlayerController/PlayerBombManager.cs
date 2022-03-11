using GamedevsToolbox.ScriptableArchitecture.Values;
using SpaceShooter.Extensions;
using SpaceShooter.WeaponsAndBullets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SpaceShooter.Actors
{
    public class PlayerBombManager : PausableObject
    {
        [SerializeField]
        private ScriptableIntReference bombCountReference = default;

        [SerializeField]
        private EmitterAbstract bombEmitter = default;

        [SerializeField]
        private UnityEvent OnBombEmitted = default;

        public void LaunchBomb(InputAction.CallbackContext context)
        {
            if (context.started && !paused)
            {
                if (bombCountReference.GetValue() > 0)
                {
                    bombCountReference.SetValue(bombCountReference.GetValue() - 1);
                    bombEmitter.Emit();
                    OnBombEmitted?.Invoke();
                }
            }
        }
    }
}