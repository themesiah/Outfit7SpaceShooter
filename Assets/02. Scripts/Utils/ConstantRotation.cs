using UnityEngine;

namespace SpaceShooter.Obtainables
{
    public class ConstantRotation : MonoBehaviour
    {
        [SerializeField]
        private Vector3 rotationVelocity = default;

        private void Update()
        {
            Vector3 euler = transform.localEulerAngles;
            euler += rotationVelocity * Time.deltaTime;
            transform.localEulerAngles = euler;
        }
    }
}