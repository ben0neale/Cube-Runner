using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighScores : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NormalScore;
    [SerializeField] TextMeshProUGUI DodgeScore;
    [SerializeField] TextMeshProUGUI JumpScore;

    // Start is called before the first frame update
    void Start()
    {
        NormalScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        DodgeScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore 3", 0).ToString();
        JumpScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore 2", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
