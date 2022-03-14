using SpaceShooter.Scoring;
using UnityEngine;

namespace SpaceShooter.UI
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField]
        private ScoreShow[] scoresShow = default;

        [SerializeField]
        private HighScores highScores = default;

        private void Start()
        {
            var scores = highScores.LoadHighScores();
            for (int i = 0; i < scores.Count; ++i)
            {
                scoresShow[i].SetScore(i + 1, scores[i].name, scores[i].score, scores[i].wave);
            }
        }
    }
}