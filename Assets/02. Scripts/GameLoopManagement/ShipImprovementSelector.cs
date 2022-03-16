using GamedevsToolbox.ScriptableArchitecture.Events;
using GamedevsToolbox.ScriptableArchitecture.Values;
using SpaceShooter.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Management
{
    public class ShipImprovementSelector : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private ScriptableFloatReference fuelReference = default;

        [SerializeField]
        private RuntimeSingleShipImprovement shipImprovementReference = default;

        [Header("Configuration")]
        [SerializeField]
        private float fuelToObtain = 30f;

        [Header("Events")]
        [SerializeField]
        private GameEvent improvementSelectionEvent = default;

        [SerializeField]
        private GameEvent waveStartEvent = default;

        public void WaveFinished()
        {
            if (shipImprovementReference.Get().CanImprove())
            {
                Debug.Log("Showing improvement selection UI");
                improvementSelectionEvent?.Raise();
            } else
            {
                GetFuel();
            }
        }

        public void ImproveShip()
        {
            Debug.Log("Selected improve ship");
            shipImprovementReference.Get().AddImprovement();
            waveStartEvent?.Raise();
        }

        public void GetFuel()
        {
            Debug.Log("Selected get fuel");
            fuelReference.SetValue(fuelReference.GetValue() + fuelToObtain);
            waveStartEvent?.Raise();
        }
    }
}