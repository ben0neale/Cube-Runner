using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] AudioSource ClickSound;
    private GameObject startTimer;
    public Animator Transition;

    private void Start()
    {
        Time.timeScale = 1f;

        if (GameObject.Find("StartTimer") != null)
            startTimer = GameObject.Find("StartTimer");
    }

    private void Update()
    {
        if (PlayerMovement.loadscene)
        {
            PlayerMovement.loadscene = false;
            StartCoroutine(transitioning(2));
        }
    }

    public enum GameMode
    {
        Normal,
        Jump,
        dodge
    }

    public static GameMode gameMode;

    public void StartButton()
    {
        NotInteractable();
        SceneManager.LoadScene(1);
        ClickSound.Play();
    }

    private void NotInteractable()
    {
        GameObject.Find("EventSystem").SetActive(false);
    }

    public void PauseButton()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0f;

            startTimer.SetActive(false);
            PausePanel.SetActive(true);

            ClickSound.Play();
        }
        else
        {
            Time.timeScale = 1f;

            startTimer.SetActive(true);
            PausePanel.SetActive(false);

            ClickSound.Play();
        }
    }

    public void Restart()
    {
        NotInteractable();
        ClickSound.Play();
        StartCoroutine(transitioning(1));
    }

    public void QuitButton()
    {
        NotInteractable();
        Time.timeScale = 1f;
        ClickSound.Play();
        StartCoroutine(transitioning(0));
    }

    public void GameModeNormal()
    {
        NotInteractable();
        gameMode = GameMode.Normal;
        ClickSound.Play();
        StartCoroutine(transitioning(1));
    }

    public void GameModeJump()
    {
        NotInteractable();
        gameMode = GameMode.Jump;
        ClickSound.Play();
        StartCoroutine(transitioning(1));
    }

    public void GameModeDodge()
    {
        NotInteractable();
        gameMode = GameMode.dodge;
        ClickSound.Play();
        StartCoroutine(transitioning(1));
    }

    public IEnumerator transitioning(int scene)
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);
    }

}
