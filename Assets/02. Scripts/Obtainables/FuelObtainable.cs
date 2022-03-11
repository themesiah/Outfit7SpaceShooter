using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace SpaceShooter.Obtainables
{
    public class FuelObtainable : MonoBehaviour, IObtainable
    {
        [SerializeField]
        private ScriptableFloatReference fuelReference = default;

        [SerializeField]
        private float fuelObtain = 30f;

        public void Obtain()
        {
            fuelReference.SetValue(fuelReference.GetValue() + fuelObtain);
        }
    }
}