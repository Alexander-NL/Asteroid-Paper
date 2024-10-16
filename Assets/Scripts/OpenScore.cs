using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class OpenScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    private string saveFilePath;

    private void Start(){
        saveFilePath = Application.persistentDataPath + "/highscore.json";

        LoadHighScore();
    }

    private void LoadHighScore(){
        if (File.Exists(saveFilePath)){
            string json = File.ReadAllText(saveFilePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + data.highScore.ToString();
            }

            Debug.Log("High score loaded and displayed: " + data.highScore);
        }
        else{
            Debug.Log("No high score file found.");
            if (highScoreText != null){
                highScoreText.text = "High Score: 0";
            }
        }
    }
}
