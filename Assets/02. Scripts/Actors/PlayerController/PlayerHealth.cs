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
        private ScriptableFloatReference fuelMaxValueRef = default;

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
            fuelReference.SetValue(fuelMaxValueRef.GetValue());
        }

        // Called when getting fuel
        public override void Heal(int heal)
        {
            base.Heal(heal);
            fuelReference.SetValue(fuelReference.GetValue() + heal);
        }

        // Called when receiving hits or colliding with enemies
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            fuelReference.SetValue(fuelReference.GetValue() - damage);
        }

        private void OnFuelChanged(float newValue)
        {
            if (newValue <= 0f)
            {
                // If fuel reaches 0 the player dies
                Die();
            }
        }
    }
}