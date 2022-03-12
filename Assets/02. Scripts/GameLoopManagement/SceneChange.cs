using GamedevsToolbox.ScriptableArchitecture.Events;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Management
{
    [CreateAssetMenu(menuName = "Space Shoter/Scene Manager")]
    public class SceneChange : ScriptableObject
    {
        [SerializeField]
        private AssetReference[] scenes = default;

        [SerializeField]
        private GameEvent OnSceneChange = default;

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ChangeAddressableScene(int sceneIndex)
        {
            OnSceneChange?.Raise();
            var op = scenes[sceneIndex].LoadSceneAsync();
        }
    }
}