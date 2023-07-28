using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScoreData", fileName = "ScoreData")]
public class ScoreData : ScriptableObject
{
    [SerializeField] private int maxHighScores = 10;
    [SerializeField] private string saveFileName = "HighScore.sav";
    private List<int> highScore = new List<int>();

    private string savePath;
    
    private void OnEnable()
    {
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        LoadHighScore();
    }
    
    public (bool, int) AddScore(int score_)
    {
        highScore.Add(score_);
        highScore.Sort();
        highScore.Reverse();

        int scoreIndex = highScore.IndexOf(score_);
        bool isNewHighScore = scoreIndex >= 0 && scoreIndex < maxHighScores;

        // Trim the high score list to keep only the top maxHighScores scores
        if (highScore.Count > maxHighScores)
        {
            highScore = highScore.GetRange(0, maxHighScores);
        }

        return (isNewHighScore, scoreIndex + 1);
    }
    
    public void ResetHighScore()
    {
        highScore.Clear();
    }
    
    public List<int> GetHighScoreList()
    {
        if (highScore.Count < maxHighScores)
        {
            var _numZerosToAdd = maxHighScores - highScore.Count;
            highScore.AddRange(new int[_numZerosToAdd]);
        }
        else if (highScore.Count > maxHighScores)
        {
            highScore = highScore.GetRange(0, maxHighScores);
        }

        return highScore;
    }
    
    public int GetHighScore() => highScore.Count > 0 ? highScore[0] : 0;
    
    public int GetHighScore(int index_) => highScore.Count > index_ ? highScore[index_] : 0;
    
    public void SaveHighScore()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fileStream = File.Create(savePath);

            formatter.Serialize(fileStream, highScore.ToArray());

            fileStream.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    
    public void LoadHighScore()
    {
        try
        {
            if (!File.Exists(savePath)) return;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(savePath, FileMode.Open);
            int[] _highScore = (int[]) formatter.Deserialize(fileStream);
            fileStream.Close();

            highScore = new List<int>(_highScore);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
