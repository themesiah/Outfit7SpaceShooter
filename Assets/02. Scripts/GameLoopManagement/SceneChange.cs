using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Management
{
    public class SceneChange : MonoBehaviour
    {
        // In the case of a bigger game, this would load the scene asynchronously and only show it when finished loading.
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}