using UnityEngine;

namespace SpaceShooter.Scenario
{
    public class SkyboxRotator : MonoBehaviour
    {
        [SerializeField]
        private Material skyboxMaterial = default;

        [SerializeField]
        private float rotationSpeed = default;

        private float currentRotation = 0f;

        private void Update()
        {
            currentRotation = (currentRotation + rotationSpeed * Time.deltaTime) % 360f;
            skyboxMaterial.SetFloat("_Rotation", currentRotation);
        }
    }
}