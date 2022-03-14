using GamedevsToolbox.ScriptableArchitecture.Events;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace SpaceShooter.UI
{
    public class ScoreName : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField = default;

        [SerializeField]
        private UnityEvent OnCorrectName = default;

        [SerializeField]
        private UnityEvent OnIncorrectName = default;

        [SerializeField]
        private StringGameEvent returnNameEvent = default;

        public void OnValueChanged(string name)
        {
            if (name.Length == 3)
            {
                OnCorrectName?.Invoke();
            } else
            {
                OnIncorrectName?.Invoke();
            }
        }

        public void OnSendName()
        {
            UnityEngine.Assertions.Assert.AreEqual(3, inputField.text.Length);
            returnNameEvent?.Raise(inputField.text);
        }
    }
}