using GamedevsToolbox.ScriptableArchitecture.Events;
using GamedevsToolbox.ScriptableArchitecture.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Scoring
{
    public class EndGameScore : MonoBehaviour
    {
        private static int MAX_SCORES_KEEP = 10;

        [SerializeField]
        private ScriptableIntReference scoreReference = default;
        [SerializeField]
        private ScriptableIntReference waveReference = default;
        [SerializeField]
        private GameEvent askForNameEvent = default;
        [SerializeField]
        private HighScores highScores = default;

        public void ManageGameEndScore()
        {
            if (scoreReference.GetValue() == 0)
                return;
            List<HighScores.HighScore> highs = highScores.LoadHighScores();
            if (highs.Count < MAX_SCORES_KEEP)
            {
                askForNameEvent?.Raise();
            }
            else
            {
                foreach (var score in highs)
                {
                    if (scoreReference.GetValue() > score.score)
                    {
                        askForNameEvent?.Raise();
                    }
                }
            }
        }

        public void SetGameEndScore(string name)
        {
            HighScores.HighScore newHighScore = new HighScores.HighScore { name = name, score = scoreReference.GetValue(), wave = waveReference.GetValue() };
            var highs = highScores.LoadHighScores();
            highs.Add(newHighScore);
            highs.Sort(highScores.CompareScores);
            if (highs.Count > MAX_SCORES_KEEP)
            {
                highs.RemoveAt(10);
            }
            highScores.SaveHighScores(highs);
        }
    }
}
