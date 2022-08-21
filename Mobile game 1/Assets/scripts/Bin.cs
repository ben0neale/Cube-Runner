using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    [SerializeField] GameObject Player;

    private void Update()
    {
        transform.position = new Vector3(0, 0, Player.transform.position.z - 200);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.tag == "PowerUp"))
        {
            Destroy(other.gameObject);
        }
    }
}
