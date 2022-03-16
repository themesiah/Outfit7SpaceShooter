using UnityEngine;

namespace SpaceShooter.Actors
{
    public class ShipImprovementManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] improvementObject = default;

        private int currentImprovement = 0;

        public bool CanImprove()
        {
            return improvementObject.Length > currentImprovement;
        }

        public void AddImprovement()
        {
            Debug.Log("Adding improvement to ship");
            improvementObject[currentImprovement].SetActive(true);
            currentImprovement++;
        }
    }
}