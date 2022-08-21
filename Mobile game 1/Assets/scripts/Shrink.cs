using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : PowerUp
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
        Player.transform.localScale = new Vector3(.5f, .5f, .5f);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Player.transform.localScale = new Vector3(1, 1, 1);
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
