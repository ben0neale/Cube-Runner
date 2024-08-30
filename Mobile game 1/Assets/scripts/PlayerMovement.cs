using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] float speed = 0f;
    [SerializeField] float Nomralspeed = 1200;
    [SerializeField] float Jumpspeed = 600;
    [SerializeField] float Xspeed = 500;
    [SerializeField] int JumpHeight = 400;

    [SerializeField] TextMeshProUGUI StateText;
    [SerializeField] AudioSource JumpNoise;

    bool grounded = false;
    int changestatemini = 190;
    bool jumping = false;

    int speedChange = 1;
    public float SpeedChangeTime = 1f;
    private float speedChangeTime;

    float multiplier = 0f;

    public static GameManager.State PlayerState;

    bool isTouchDown = false;

    private void Start()
    {
        speedChangeTime = SpeedChangeTime;
        changestatemini = 190;



        if (ButtonController.gameMode == ButtonController.GameMode.Jump)
        {
            PlayerState = GameManager.State.Jump;
            speed = Jumpspeed;
        }
        else if (ButtonController.gameMode == ButtonController.GameMode.dodge)
        {
            PlayerState = GameManager.State.Dodge;
            speed = Nomralspeed;
        }
        else
        {
            PlayerState = GameManager.State.Dodge;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StateText.text = "State: " + PlayerState.ToString();
           
/*        #if UNITY_STANDALONE*/
        float x = Input.GetAxis("Horizontal");
        if (!GetComponent<PlayerDeath>().dead)
        {
            RB.velocity = new Vector3(x * Xspeed * Time.deltaTime, RB.velocity.y, speed * Time.deltaTime);
        }
/*        #endif*/
 

        StateChanges();
        


        if ((int.Parse(GameManager.ScoreText.text) - 65) % 260 == 0 && int.Parse(GameManager.ScoreText.text) > changestatemini)
        {
            changestatemini += 260;
            if (ButtonController.gameMode == ButtonController.GameMode.dodge)
            {
                Nomralspeed += speedChange;
            }
            else if (ButtonController.gameMode == ButtonController.GameMode.Jump)
            {
                Jumpspeed += speedChange;
            }
        }
    }

    private void Update()
    {
#if UNITY_IOS
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Stationary)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        //move left
                        multiplier = -1f;
                    }
                    else
                    {
                        //move right
                        multiplier = 1f;
                    }
                }
                else
                    multiplier = 0f;
            }
        }
        else
        {
            multiplier = 0f;
        }
        RB.velocity = new Vector3(multiplier * Xspeed * Time.deltaTime, RB.velocity.y, speed * Time.deltaTime);
#endif

        if (ButtonController.gameMode == ButtonController.GameMode.Normal)
            ChangeState();

        if(speedChangeTime <= 0)
        {
            speedChangeTime = SpeedChangeTime;
            SpeedChange();
        }
        else
            speedChangeTime -= Time.deltaTime;
    }

    public void jump()
    {

#if UNITY_IOS
        //jump

        if (PlayerState == GameManager.State.Jump && grounded)
        {
            print("JIMPINGGGGGG");
            RB.AddForce(0, JumpHeight, 0);
            JumpNoise.Play();
        }

#endif

#if UNITY_STANDALONE
        if (PlayerState == GameManager.State.Jump && grounded && Input.GetKeyDown(KeyCode.Space))
        {
            RB.AddForce(0, JumpHeight, 0);
            JumpNoise.Play();
        }
#endif
    }

   
    private void SpeedChange()
    {
        Nomralspeed += speedChange;
        Jumpspeed += speedChange;
    }

    private void StateChanges()
    {
        switch (PlayerState)
        {
            case GameManager.State.Dodge:
                speed = Nomralspeed;
                break;
            case GameManager.State.Jump:
                speed = Jumpspeed;
                break;
        }
    }

    private void ChangeState()
    {
        if ((int.Parse(GameManager.ScoreText.text) - 65) % 260 == 0 && int.Parse(GameManager.ScoreText.text) > changestatemini)
        {
            changestatemini += 260;
            if (PlayerState == GameManager.State.Dodge)
            {
                PlayerState = GameManager.State.Jump;
                //Nomralspeed += speedChange;
            }
            else if (PlayerState == GameManager.State.Jump)
            {
                PlayerState = GameManager.State.Dodge;
                //Jumpspeed += speedChange;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            jumping = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            other.GetComponent<PowerUp>().Activate();
        }
    }
}
