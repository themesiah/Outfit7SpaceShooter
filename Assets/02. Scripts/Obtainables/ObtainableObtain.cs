using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Obtainables
{
    // This behaviour requires an IObtainable behaviour in the same object (NOT IN THE COLLIDED OBJECT!)
    public class ObtainableObtain : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnObtained = default;

        private void OnTriggerEnter(Collider other)
        {
            // Not checking for tags because the physics collision matrix already manages colliding with only the necessary objects
            gameObject.GetComponent<IObtainable>()?.Obtain();
            OnObtained?.Invoke();
        }
    }
}