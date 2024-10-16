using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int score;
    public int highScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI newHighScoreText;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        saveFilePath = Application.persistentDataPath + "/highscore.json";

        LoadHighScore();
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        Debug.Log("Updated Score: " + score);

        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();

            if (newHighScoreText != null)
            {
                newHighScoreText.gameObject.SetActive(true);
            }
        }
        else
        {
            if (newHighScoreText != null)
            {
                newHighScoreText.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
           if (newHighScoreText != null && score < highScore)
            {
                newHighScoreText.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        if (scoreText != null)
        {
            DisplayScore(score);
        }
    }

    private void SaveHighScore()
    {
        HighScoreData data = new HighScoreData { highScore = highScore };
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
        Debug.Log("High score saved to: " + saveFilePath);
    }

    private void LoadHighScore()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            highScore = data.highScore;
            Debug.Log("High score loaded: " + highScore);
        }
        else
        {
            Debug.Log("No high score file found. Starting fresh.");
            highScore = 0;
        }
    }
}

[System.Serializable]
public class HighScoreData
{
    public int highScore;
}
