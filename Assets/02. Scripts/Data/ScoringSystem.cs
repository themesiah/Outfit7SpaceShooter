using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Values;

namespace SpaceShooter.Data
{
    public class ScoringSystem : MonoBehaviour
    {
        [SerializeField]
        private ScriptableIntReference scoreReference = default;

        [SerializeField]
        private float comboExtraMultiplier = 0.02f;

        [SerializeField]
        private int maxCombo = 50;

        [SerializeField]
        private ScriptableIntReference currentComboReference = default;

        public void OnAddScore(int score)
        {
            // With a base multiplier of 1, we add up to 1 to the multiplier (x2 multiplier) to get the score we are adding
            int totalAdded = (int)(score * (1f + currentComboReference.GetValue() * comboExtraMultiplier));
            // The score reference will trigger events, so we are done
            scoreReference.SetValue(scoreReference.GetValue() + totalAdded);
        }

        public void OnComboAdded()
        {
            // We add a combo to the counter up to 50
            currentComboReference.SetValue(System.Math.Min(currentComboReference.GetValue() + 1, maxCombo));
        }

        public void OnComboFinished()
        {
            // Called when we are hit, this sets the combo to 0 (x1 multiplier) again
            currentComboReference.SetValue(0);
        }
    }
}