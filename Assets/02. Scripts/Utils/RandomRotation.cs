using GamedevsToolbox.ScriptableArchitecture.Values;
using UnityEngine;

namespace SpaceShooter.Obtainables
{
    public class RandomRotation : MonoBehaviour
    {
        [SerializeField]
        private ScriptableFloatReference rotationSpeed = default;

        Vector3 currentRotation;
        Vector3 rotationDirection;

        private void Awake()
        {
            rotationDirection = new Vector3(0f, Random.value, Random.value).normalized;
            currentRotation = rotationDirection * 360f;
            transform.localEulerAngles = currentRotation;
        }

        private void Update()
        {
            currentRotation += rotationSpeed.GetValue() * Time.deltaTime * rotationDirection;
            transform.localEulerAngles = currentRotation;
        }
    }
}