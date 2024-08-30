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
    [SerializeField] float Xspeed = 500;
    [SerializeField] int JumpHeight = 400;

    [SerializeField] TextMeshProUGUI StateText;
    [SerializeField] AudioSource JumpNoise;

    bool grounded = false;

    bool jumping = false;

    int speedChange = 1;
    public float SpeedChangeTime = 1f;
    private float speedChangeTime;

    float multiplier = 0f;

    bool isTouchDown = false;

    private void Start()
    {
        speedChangeTime = SpeedChangeTime;
        //Set active state dependent on selected gamemode
        if (GameManager.gameMode == GameManager.GameMode.Jump) { PlayerStateChange.PlayerState = GameManager.State.Jump; }
            
        else if (GameManager.gameMode == GameManager.GameMode.dodge) { PlayerStateChange.PlayerState = GameManager.State.Dodge; }
            
        else
            PlayerStateChange.PlayerState = GameManager.State.Dodge;

        speed = Nomralspeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StateText.text = "State: " + PlayerStateChange.PlayerState.ToString();

        //When in normal mode, change between jump and dodge states
        if (GameManager.gameMode == GameManager.GameMode.Normal) { GetComponent<PlayerStateChange>().ChangeState(); }
           
        //Increase Speed at interval
        if (speedChangeTime <= 0)
        {
            speedChangeTime = SpeedChangeTime;
            SpeedIncrease();
        }
        else
            speedChangeTime -= Time.deltaTime;

        // IOS ONLY -------------------------------------------------------------------------------------------

#if UNITY_IOS
        IOSControls();
#endif

        // WINDOWS ONLY + TESTING --------------------------------------------------------------------------------
        WindowsControls();
    }

    public void jump()
    {
        if (PlayerStateChange.PlayerState == GameManager.State.Jump && grounded)
        {
            RB.AddForce(0, JumpHeight, 0);
            JumpNoise.Play();
        }
    }

    private void SpeedIncrease()
    {
        Nomralspeed += speedChange;
    }

    private void IOSControls()
    {
        //Touch movenet left and right - Swipt to jump control from another script
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
    }

    private void WindowsControls()
    {
        float x = Input.GetAxis("Horizontal");
        if (!GetComponent<PlayerDeath>().dead)
        {
            RB.velocity = new Vector3(x * Xspeed * Time.deltaTime, RB.velocity.y, speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)) { jump(); }
    }


    /// COLLISION DETECTION -----------------------------------------------------------------------------------------

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
