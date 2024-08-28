using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource InGame;
    [SerializeField] AudioSource menu;
    bool speed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && !InGame.isPlaying)
        {
            InGame.Play();
        }
        else if(!(SceneManager.GetActiveScene().buildIndex == 1) && !menu.isPlaying)
        {
            menu.Play();
        }
        if (speed == false && SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(SoundSpeed());
        }

    }

    IEnumerator SoundSpeed()
    {
        speed = true;

        yield return new WaitForSeconds(10);

        //InGame.pitch += .05f;
        speed = false;
    }
}
