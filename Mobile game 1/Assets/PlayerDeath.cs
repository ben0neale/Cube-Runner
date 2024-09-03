using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] GameObject continueScreen;
    [SerializeField] GameObject BrokenPlayer;
    [SerializeField] ScoreTextController scoreTextController;
    [SerializeField] ButtonController buttonController;
    public int FinalScore = 0;
    public bool dead = false;

    private bool hasDied = false;
    bool deathExplosion = false;
    bool rewardChance = false;

    // Start is called before the first frame update
    void Start()
    {
        FinalScore = 0;
        dead = false;
        deathExplosion = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -5)
        {
            dead = true;
        }

        if (dead && !hasDied)
        {
            hasDied = true;
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        FinalScore = int.Parse(GameManager.ScoreText.text);

        if (!deathExplosion)
        {
            transform.localScale = new Vector3(0, 0, 0);
            Instantiate(BrokenPlayer, transform.position, Quaternion.identity);
            deathExplosion = true;
        }

        yield return new WaitForSeconds(1.5f);

        if (!rewardChance)
        {
            rewardChance = true;
            GameObject.Find("Ads").GetComponent<RewardAds>().LoadAd();
            continueScreen.SetActive(true);
        }
        else
            ButtonControllerLoadScene();
    }

    public void ButtonControllerLoadScene()
    {
        buttonController.transitioning(2);
    }

    public void Continue()
    {
        dead = false;
        hasDied = false;
        deathExplosion = false;
        continueScreen.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstical" && !dead)
        {
            dead = true;
        }
    }
}
