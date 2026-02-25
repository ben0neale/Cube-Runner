using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScreen : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
            StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        Player.GetComponent<PlayerDeath>().ButtonControllerLoadScene();
    }
}
