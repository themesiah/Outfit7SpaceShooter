using UnityEngine;
using TMPro;
using SpaceShooter.Extensions;
using GamedevsToolbox.ScriptableArchitecture.Pools;

namespace SpaceShooter.StressTest
{
    public class BulletCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI fpsCountReference = default;

        [SerializeField]
        private RuntimeSingleBulletPoolContainer bulletPoolContainerReference = default;

        private ScriptablePool poolReference;

        private void Start()
        {
            poolReference = (ScriptablePool)bulletPoolContainerReference.Get().pool;
        }

        private void Update()
        {
            fpsCountReference.text = string.Format("{0} / {1}", poolReference.UsedCount, poolReference.UsedCount + poolReference.FreeCount);
        }
    }
}