using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreTextController : MonoBehaviour
{
    GameObject Player;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI HighScore;

    private void Start()
    {
        Player = GameObject.Find("Player");

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
        score.text = Player.GetComponent<PlayerDeath>().FinalScore.ToString();

        if (Player.GetComponent<PlayerDeath>().FinalScore > PlayerPrefs.GetInt("HighScore", 0) && ButtonController.gameMode == ButtonController.GameMode.Normal)
        {
            PlayerPrefs.SetInt("HighScore", Player.GetComponent<PlayerDeath>().FinalScore);
            HighScore.text = Player.GetComponent<PlayerDeath>().FinalScore.ToString();
        }
        else if (Player.GetComponent<PlayerDeath>().FinalScore > PlayerPrefs.GetInt("HighScore 2", 0) && ButtonController.gameMode == ButtonController.GameMode.Jump)
        {
            PlayerPrefs.SetInt("HighScore 2", Player.GetComponent<PlayerDeath>().FinalScore);
            HighScore.text = Player.GetComponent<PlayerDeath>().FinalScore.ToString();
        }
        else if((Player.GetComponent<PlayerDeath>().FinalScore > PlayerPrefs.GetInt("HighScore 3", 0) && ButtonController.gameMode == ButtonController.GameMode.dodge))
        {
            PlayerPrefs.SetInt("HighScore 3", Player.GetComponent<PlayerDeath>().FinalScore);
            HighScore.text = Player.GetComponent<PlayerDeath>().FinalScore.ToString();
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
