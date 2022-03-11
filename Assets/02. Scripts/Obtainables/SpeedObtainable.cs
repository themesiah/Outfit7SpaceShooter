using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace SpaceShooter.Obtainables
{
    public class SpeedObtainable : MonoBehaviour, IObtainable
    {
        [SerializeField]
        private ScriptableFloatReference speedReference = default;

        [SerializeField]
        private float extraSpeed = 5f;

        public void Obtain()
        {
            speedReference.SetValue(speedReference.GetValue() + extraSpeed);
        }
    }
}