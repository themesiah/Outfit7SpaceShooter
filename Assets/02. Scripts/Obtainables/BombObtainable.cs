using GamedevsToolbox.ScriptableArchitecture.Values;
using UnityEngine;

namespace SpaceShooter.Obtainables
{
    public class BombObtainable : MonoBehaviour
    {
        [SerializeField]
        private ScriptableIntReference bombCountReference = default;

        public void Obtain()
        {
            // Always get 1 bomb
            bombCountReference.SetValue(bombCountReference.GetValue() + 1);
        }
    }
}