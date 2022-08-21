using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] AudioSource ClickSound;
    public Animator Transition;

    private void Start()
    {
        Time.timeScale = 1f;
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
        SceneManager.LoadScene(1);
        ClickSound.Play();
    }

    public void PauseButton()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            ClickSound.Play();
        }
        else
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            ClickSound.Play();
        }
    }

    public void Restart()
    {
        ClickSound.Play();
        StartCoroutine(transitioning(1));
    }

    public void QuitButton()
    {
        Time.timeScale = 1f;
        ClickSound.Play();
        StartCoroutine(transitioning(0));
    }

    public void GameModeNormal()
    {
        gameMode = GameMode.Normal;
        ClickSound.Play();
        StartCoroutine(transitioning(1));
    }

    public void GameModeJump()
    {
        gameMode = GameMode.Jump;
        ClickSound.Play();
        StartCoroutine(transitioning(1));
    }

    public void GameModeDodge()
    {
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
