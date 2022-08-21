using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Platform;
    [SerializeField] GameObject Player;
    [SerializeField] Rigidbody PlayerRB;
    [SerializeField] GameObject[] Presets;
    [SerializeField] GameObject[] JumpPresets;
    [SerializeField] GameObject[] PowerUps;
    [SerializeField] GameObject InterimObj;
    [SerializeField] public static TextMeshProUGUI ScoreText;
    [SerializeField] GameObject StartCountdown;
    [SerializeField] TextMeshProUGUI StartCountDownTXT;
    float startTimer = 1f;
    bool Starting = false;
    int timer = 3;

    public static string PlayerName = "Player";
    public static bool StateChange = false;
    public static int ChangeStateDistance = 200;
    public static int ChangeStateMin;

    float platformYpos;

    public enum State
    {
        Dodge,
        Jump,
        Gravity
    }

    public static State GameState;

    float PlatformCount;
    float PlayerStartZ;
    int PlatformspawncountTemp;
    int InterimCount;
    int interimDist = 40;

    private void Start()
    {
        Time.timeScale = 1f;
        PlayerStartZ = Player.transform.position.z;
        ScoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        ChangeStateMin = 150;
        GameState = State.Dodge;
        PlatformCount = 1f;
        PlatformspawncountTemp = 0;
        InterimCount = 0;
        platformYpos = .01f;

        if (ButtonController.gameMode == ButtonController.GameMode.Jump)
        {
            GameState = State.Jump;
        }
        else if (ButtonController.gameMode == ButtonController.GameMode.dodge)
        {
            GameState = State.Dodge;
        }
        else
        {
            GameState = State.Dodge;
        }

        PlayerRB.constraints = RigidbodyConstraints.FreezeAll;
        StartCountdown.SetActive(true);
        StartCountDownTXT.text = timer.ToString();
        Starting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonController.gameMode == ButtonController.GameMode.Normal)
        {
            ChangeState();
        }
        StartState();
        LevelGeneration();

        if (!PlayerMovement.dead)
        {
            GameManager.ScoreText.text = ((int)(Player.transform.position.z - PlayerStartZ)).ToString();
        } 
    }

    private void LevelGeneration()
    {
        switch (GameState)
        {
            case State.Dodge:
                if (Player.transform.position.z - PlayerStartZ > (PlatformCount - 2) * 130)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int random = Random.Range(0, Presets.Length);
                        Instantiate(Presets[random], new Vector3(0, -platformYpos, ((PlatformCount - .5f) * 130) + 16.25f + (i * 32.5f)), Quaternion.identity);
                        platformYpos += .001f;
                    }

                   // int random1 = Random.Range(0, PowerUps.Length);
                    //Instantiate(PowerUps[random1], new Vector3(Random.Range(-10, 10), .5f, ((PlatformCount + 1) * 130) + Random.Range(0, 130)), Quaternion.identity);

                    PlatformCount++;
                    PlatformspawncountTemp++;
                    platformYpos += .01f;

                }
                break;
            case State.Jump:
                if (Player.transform.position.z - PlayerStartZ > (PlatformCount - 2) * 130)
                {
                    int rand = Random.Range(0, JumpPresets.Length);
                    Instantiate(JumpPresets[rand], new Vector3(0, -1 - platformYpos, ((PlatformCount) * 130)), Quaternion.identity);

                    //int random1 = Random.Range(0, PowerUps.Length);
                    //Instantiate(PowerUps[random1], new Vector3(Random.Range(-10, 10), .5f - platformYpos, ((PlatformCount + 1) * 130) + Random.Range(0, 130)), Quaternion.identity);

                    PlatformCount++;
                    PlatformspawncountTemp++;
                    platformYpos += .001f;
                }
                break;
            case State.Gravity:
                break;

        }
    }

    private void StartState()
    {

        if (Starting)
        {
            if (startTimer > 0f)
            {
                startTimer -= Time.deltaTime;
            }
            else
            {
                startTimer = 1f;
                timer--;
                StartCountDownTXT.text = timer.ToString();
            }

            if (timer == 0)
            {
                Starting = false;
                StartCountdown.SetActive(false);
                PlayerRB.constraints = RigidbodyConstraints.None;
                PlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }


    }

    private void ChangeState()
    {
        if (PlatformspawncountTemp == 2)
        {
            //Instantiate(InterimObj, new Vector3(0, -1, ((PlatformCount) * 100) + (InterimCount * interimDist) - 30), Quaternion.identity);
            InterimCount++;
            if (GameState == State.Dodge)
                GameState = State.Jump;
            else if (GameState == State.Jump)
                GameState = State.Dodge;
            PlatformspawncountTemp = 0;
        }
    }
}
