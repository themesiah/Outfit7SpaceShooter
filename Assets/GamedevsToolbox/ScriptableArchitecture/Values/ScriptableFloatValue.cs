using UnityEngine;

namespace GamedevsToolbox.ScriptableArchitecture.Values
{
    [CreateAssetMenu(menuName = "Scriptable Architecture/Values/Float")]
    [System.Serializable]
    public class ScriptableFloatValue : ScriptableValue<float>
    {
        [SerializeField]
        private float maxValue = default;

        [SerializeField]
        private bool useMaxValue = false;

        #region Public Methods
        public void IncrementValue(float increment)
        {
            SetValue(GetValue() + increment);
        }

        public override void SetValue(float value)
        {
            if (useMaxValue && value > maxValue)
                base.SetValue(maxValue);
            else
                base.SetValue(value);
        }
        #endregion
    }
}