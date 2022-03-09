using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace SpaceShooter.Actors
{
    // For the player, the health component manages its fuel. For the player, the "fuel" is like the health.
    public class PlayerHealth : CharacterHealth
    {
        [SerializeField]
        private ScriptableFloatReference fuelReference = default;

        [SerializeField]
        private float fuelMaxValue = default;

        private void OnEnable()
        {
            fuelReference.RegisterOnChangeAction(OnFuelChanged);
        }

        private void OnDisable()
        {
            fuelReference.UnregisterOnChangeAction(OnFuelChanged);
        }

        private void Awake()
        {
            fuelReference.SetValue(fuelMaxValue);
        }

        // Called when getting fuel
        public override void Heal(int heal)
        {
            fuelReference.SetValue(fuelReference.GetValue() + heal);
        }

        // Called when receiving hits or colliding with enemies
        public override void TakeDamage(int damage)
        {
            fuelReference.SetValue(fuelReference.GetValue() - damage);
        }

        private void OnFuelChanged(float newValue)
        {
            if (fuelReference.GetValue() <= 0f)
            {
                // If fuel reaches 0 the player dies
                Die();
            }
            else if (fuelReference.GetValue() > fuelMaxValue)
            {
                // Clamp fuel value
                fuelReference.SetValue(fuelMaxValue);
            }
        }
    }
}