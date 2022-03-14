using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Scoring
{
    public class HighScores : MonoBehaviour
    {
        private static string SCORES_FILE_NAME = "score.json";

        [System.Serializable]
        public struct HighScore
        {
            public string name;
            public int score;
            public int wave;
        }

        // This class allows us to serialize into json
        [System.Serializable]
        public class HighScoreHolder
        {
            public List<HighScore> highScoreList;
        }

        [SerializeField]
        [HideInInspector]
        private HighScoreHolder highScoreHolder;

        public List<HighScore> LoadHighScores()
        {
            if (highScoreHolder == null || highScoreHolder.highScoreList.Count == 0)
            {
                string jsonData = GamedevsToolbox.Utils.Utils.LoadFile(SCORES_FILE_NAME);
                Debug.Log(jsonData);
                if (string.IsNullOrEmpty(jsonData))
                {
                    highScoreHolder = new HighScoreHolder();
                    highScoreHolder.highScoreList = new List<HighScore>();
                }
                else
                {
                    highScoreHolder = JsonUtility.FromJson<HighScoreHolder>(jsonData);
                    highScoreHolder.highScoreList.Sort(CompareScores);
                }
            }
            return highScoreHolder.highScoreList;
        }

        public void SaveHighScores(List<HighScore> highScoreList)
        {
            highScoreHolder.highScoreList = highScoreList;
            string jsonData = JsonUtility.ToJson(highScoreHolder);
            Debug.Log(jsonData);
            GamedevsToolbox.Utils.Utils.SaveFile(SCORES_FILE_NAME, jsonData);
        }

        public int CompareScores(HighScores.HighScore score1, HighScores.HighScore score2)
        {
            return score2.score - score1.score;
        }

    }
}