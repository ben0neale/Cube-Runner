using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : PowerUp
{
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find(GameManager.PlayerName);
        TimeLasted = 5;
    }

    public override void Activate()
    {
        base.Activate();
        Time.timeScale = .7f;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            TimeLasted -= Time.deltaTime;
        }
        if (TimeLasted < 0)
        {
            Deactivate();
            TimeLasted = 5;
        }
    }
}
