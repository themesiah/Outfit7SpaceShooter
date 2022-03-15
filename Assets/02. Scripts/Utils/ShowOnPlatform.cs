using UnityEngine;

namespace SpaceShooter.Utils
{
#pragma warning disable CS0414 // Variable declared but not used. We don't want any warning, but we are not doing anything wrong.
    public class ShowOnPlatform : MonoBehaviour
    {
        [SerializeField]
        private bool showOnAndroid = false;
        [SerializeField]
        private bool showOnDesktop = false;

        private void Start()
        {
            gameObject.SetActive(false);
#if UNITY_ANDROID
            if (showOnAndroid)
            {
                gameObject.SetActive(true);
            }
#else
            if (showOnDesktop)
            {
                gameObject.SetActive(true);
            }
#endif
        }
    }
}
#pragma warning restore CS0414