using UnityEngine;
using UnityEngine.UI;
using GamedevsToolbox.ScriptableArchitecture.Values;
using SpaceShooter.Utils;

namespace SpaceShooter.UI
{
    public class FuelSlider : MonoBehaviour
    {
        [SerializeField]
        private ScriptableFloatReference fuelRef = default;
        [SerializeField]
        private ScriptableFloatReference maxFuelRef = default;
        [SerializeField]
        private Slider fuelSlider = default;

        private void OnEnable()
        {
            fuelRef.RegisterOnChangeAction(OnFuelChanged);
        }

        private void OnDisable()
        {
            fuelRef.UnregisterOnChangeAction(OnFuelChanged);
        }

        private void OnFuelChanged(float fuel)
        {
            if (SimpleLogger.Instance)
            SimpleLogger.Instance.Log(SimpleLogger.LogContext.UI, "Fuel changed: {0}", fuel);
            fuelSlider.value = fuel / maxFuelRef.GetValue();
        }
    }
}