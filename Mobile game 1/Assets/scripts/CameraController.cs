using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject Player;
    [SerializeField] Transform PlayerTrans;
    Vector3 Offset = new Vector3(0, 4f, -8f);

    private void Start()
    {
        if (GameObject.Find("Player") != null)
            Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Player.GetComponent<PlayerDeath>().dead)
        {
            transform.position = new Vector3(transform.position.x, 0, PlayerTrans.position.z) + Offset;
            transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerTrans.position.x, transform.position.y, transform.position.z), 2f * Time.deltaTime);
        }

    }
}
