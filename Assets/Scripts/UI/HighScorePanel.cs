using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HighScorePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] highScoreTxtArr;
        [SerializeField] private ScoreData scoreData;

        private void Awake()
        {
            if(highScoreTxtArr.Length != scoreData.GetHighScoreList().Count)
                throw new Exception("HighScorePanel: highScoreTxtArr.Length != scoreData.highScoreArr.Length");
        }

        private void OnEnable()
        {
            DisplayHighScore();
        }

        private void DisplayHighScore()
        {
            var _highScoreList = scoreData.GetHighScoreList();
            for (var i = 0; i < _highScoreList.Count; i++)
            {
                highScoreTxtArr[i].text = $"{i+1}.  {_highScoreList[i]}";
            }
        }
    }
}
