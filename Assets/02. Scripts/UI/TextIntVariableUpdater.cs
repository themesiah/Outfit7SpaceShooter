using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;
using TMPro;

namespace SpaceShooter.UI
{
    // This class automatically sets a text with a parameter {0} with an int variable any time it changes
    public class TextIntVariableUpdater : MonoBehaviour
    {
        [SerializeField]
        private ScriptableIntReference intVariableReference = default;

        [SerializeField]
        private TextMeshProUGUI textReference = default;

        [SerializeField]
        private string formatText = default;

        private void OnEnable()
        {
            intVariableReference.RegisterOnChangeAction(OnVariableChanged);
        }

        private void OnDisable()
        {
            intVariableReference.UnregisterOnChangeAction(OnVariableChanged);
        }

        private void Awake()
        {
            ChangeText(intVariableReference.GetValue());
        }

        private void OnVariableChanged(int newVariable)
        {
            ChangeText(newVariable);
        }

        private void ChangeText(int variable)
        {
            textReference.text = string.Format(formatText, variable);
        }
    }
}