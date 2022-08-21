using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform PlayerTrans;
    Vector3 Offset = new Vector3(0, 4f, -8f);

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerMovement.dead)
        {
            transform.position = new Vector3(transform.position.x, 0, PlayerTrans.position.z) + Offset;
            transform.position = Vector3.Lerp(transform.position, new Vector3(PlayerTrans.position.x, transform.position.y, transform.position.z), 2f * Time.deltaTime);
        }

    }
}
