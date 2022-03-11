using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace SpaceShooter.Obtainables
{
    public class PowerupObtainable : MonoBehaviour, IObtainable
    {
        [SerializeField]
        private ScriptableIntReference weaponDamageReference = default;

        [SerializeField]
        private int additionalDamage = 5;

        public void Obtain()
        {
            weaponDamageReference.SetValue(weaponDamageReference.GetValue() + additionalDamage);
        }
    }
}