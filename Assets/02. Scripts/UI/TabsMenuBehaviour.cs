using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SpaceShooter.UI
{
    public class TabsMenuBehaviour : MonoBehaviour
    {
        [System.Serializable]
        public class TabContents
        {
            [Tooltip("Only for info")]
            public string tabName;
            public GameObject[] contents;
            public UnityEvent OnTabActivated;
            public UnityEvent OnTabDeactivated;
        }

        [SerializeField]
        private TabContents[] tabs = default;
        [SerializeField]
        private bool returnToFirstTabOnEnable = default;

        private int currentTab = 0;

        private void OnEnable()
        {
            if (returnToFirstTabOnEnable)
            {
                if (currentTab != 0)
                {
                    ChangeTab(0);
                }
            }
        }

        private void Start()
        {
            DeactivateAll();
            ChangeTab(0);
        }

        public void NextTab(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ChangeTab((currentTab + 1) % tabs.Length);
            }
        }

        public void PreviousTab(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                int nextTab = currentTab - 1;
                if (nextTab < 0)
                {
                    nextTab = tabs.Length - 1;
                }
                ChangeTab(nextTab);
            }
        }

        private void DeactivateAll()
        {
            foreach(var tab in tabs)
            {
                foreach(var go in tab.contents)
                {
                    go.SetActive(false);
                }
            }
        }

        public void ChangeTab(int newTab)
        {
            if (newTab < 0 || newTab >= tabs.Length)
                return;
            foreach(GameObject go in tabs[currentTab].contents)
            {
                go.SetActive(false);
            }
            tabs[currentTab].OnTabDeactivated?.Invoke();
            foreach (GameObject go in tabs[newTab].contents)
            {
                go.SetActive(true);
            }
            tabs[newTab].OnTabActivated?.Invoke();
            currentTab = newTab;
        }
    }
}