using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.Utils
{
    // If this class doesn't set the polling frequency higher than 60, some gamepads (xinput) will have abnormal behaviour with the left stick
    public class InputPolling : MonoBehaviour
    {
        [SerializeField]
        private float pollingFrequency = 300f;
        private void Start()
        {
            Debug.LogFormat("Setting polling rate of input system to {0}", pollingFrequency);
            InputSystem.pollingFrequency = pollingFrequency;
        }
    }
}