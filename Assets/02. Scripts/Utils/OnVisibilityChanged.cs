using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Utils
{
    public class OnVisibilityChanged : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnVisible = default;

        [SerializeField]
        private UnityEvent OnInvisible = default;

        private void OnBecameVisible()
        {
            OnVisible?.Invoke();
        }

        private void OnBecameInvisible()
        {
            OnInvisible?.Invoke();
        }
    }
}