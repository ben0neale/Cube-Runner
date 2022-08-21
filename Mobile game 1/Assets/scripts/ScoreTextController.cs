using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI HighScore;

    private void Start()
    {
        if (ButtonController.gameMode == ButtonController.GameMode.Normal)
        {
            HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        else if (ButtonController.gameMode == ButtonController.GameMode.Jump)
        {
            HighScore.text = PlayerPrefs.GetInt("HighScore 2", 0).ToString();
        }
        else
        {
            HighScore.text = PlayerPrefs.GetInt("HighScore 3", 0).ToString();
        }

        UpdateScore();
    }

    void UpdateScore()
    {
        score.text = PlayerMovement.FinalScore.ToString();

        if (PlayerMovement.FinalScore > PlayerPrefs.GetInt("HighScore", 0) && ButtonController.gameMode == ButtonController.GameMode.Normal)
        {
            PlayerPrefs.SetInt("HighScore", PlayerMovement.FinalScore);
            HighScore.text = PlayerMovement.FinalScore.ToString();
        }
        else if (PlayerMovement.FinalScore > PlayerPrefs.GetInt("HighScore 2", 0) && ButtonController.gameMode == ButtonController.GameMode.Jump)
        {
            PlayerPrefs.SetInt("HighScore 2", PlayerMovement.FinalScore);
            HighScore.text = PlayerMovement.FinalScore.ToString();
        }
        else if((PlayerMovement.FinalScore > PlayerPrefs.GetInt("HighScore 3", 0) && ButtonController.gameMode == ButtonController.GameMode.dodge))
        {
            PlayerPrefs.SetInt("HighScore 3", PlayerMovement.FinalScore);
            HighScore.text = PlayerMovement.FinalScore.ToString();
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
