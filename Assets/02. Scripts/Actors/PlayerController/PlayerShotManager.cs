using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GamedevsToolbox.ScriptableArchitecture.Values;
using UnityEngine.Events;
using SpaceShooter.WeaponsAndBullets;
using SpaceShooter.Extensions;

namespace SpaceShooter.Actors
{
    public class PlayerShotManager : PausableObject
    {
        [Header("References")]
        [SerializeField]
        private ScriptableFloatReference fuelReference = default;

        [SerializeField]
        private EmitterAbstract[] emitters = default;

        [Header("Parameters")]
        [SerializeField]
        private float shotFuelConsumptionPerShot = 0.1f;

        [SerializeField]
        private float shotDelay = 0.1f;

        [Header("Events")]
        [SerializeField]
        private UnityEvent OnShotsEmitted = default;

        private bool shoting = false;
        private float shotTimer = 0f;

        public void Shot(InputAction.CallbackContext context)
        {
            if (context.started && !paused)
            {
                shoting = true;
            } else if (context.canceled || paused)
            {
                shoting = false;
            }
        }

        private void Update()
        {
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0 && shoting)
            {
                shotTimer = shotDelay;
                fuelReference.SetValue(fuelReference.GetValue() - shotFuelConsumptionPerShot);
                foreach(var emitter in emitters)
                {
                    emitter.Emit();
                }
                OnShotsEmitted?.Invoke();
            }
        }
    }
}