using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStateChange : MonoBehaviour
{
    public int changestatemini = 190;
    public static GameManager.State PlayerState;

    public void ChangeState()
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

}
