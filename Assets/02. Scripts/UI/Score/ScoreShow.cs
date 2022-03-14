using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter.UI
{
    public class ScoreShow : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textReference = default;

        [SerializeField]
        [Multiline]
        private string format = "[{0}{1}]{2} {3} Wave {4}";

        [Header("Defaults")]
        [SerializeField]
        private int positionDefault = 0;
        [SerializeField]
        private string scoreNameDefault = "";
        [SerializeField]
        private int scoreDefault = 0;
        [SerializeField]
        private int waveDefault = 0;

        private void Awake()
        {
            // If there is no score, it is empty
            textReference.text = "";
        }

        public void SetScore(int position, string scoreName, int score, int wave)
        {
            string positionSufix = "th";
            if (position == 1)
            {
                positionSufix = "st";
            } else if (position == 2)
            {
                positionSufix = "nd";
            } else if (position == 3)
            {
                positionSufix = "rd";
            }

            textReference.text = string.Format(format, position, positionSufix, scoreName, score, wave);
        }

        private void SetDefaults()
        {
            // Just to see easility in the editor
            SetScore(positionDefault, scoreNameDefault, scoreDefault, waveDefault);
        }

        private void OnValidate()
        {
            // So we can see easily the result in the editor
            SetDefaults();
        }
    }
}