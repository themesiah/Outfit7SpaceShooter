using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceShooter.UI
{
    // Set this monobehaviour on a button that must be "first selected" when appearing on screen.
    public class ButtonSelect : MonoBehaviour
    {
        [SerializeField]
        private Button button = default;

        private void OnEnable()
        {
            //if (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject.activeInHierarchy == false)
            {
                button.Select();
            }
        }
    }
}