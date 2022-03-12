using UnityEngine;
using TMPro;

namespace SpaceShooter.StressTest
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI fpsCountReference = default;
        [SerializeField]
        private float refreshTime = 0.5f;

        private int frameCounter = 0;
        private float timeCounter = 0f;
        private float lastFps = 0f;

        private void Update()
        {
            if (timeCounter < refreshTime)
            {
                timeCounter += Time.deltaTime;
                frameCounter++;
            } else
            {
                lastFps = (float)frameCounter / timeCounter;
                frameCounter = 0;
                timeCounter = 0.0f;
                fpsCountReference.text = lastFps.ToString();
            }            
        }
    }
}