using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void RewardRespawn()
    {
        GameObject[] Platforms = GameObject.FindGameObjectsWithTag("Preset");

        foreach (GameObject plat in Platforms)
        {
            foreach (Transform child in plat.transform)
            {
                if(child.CompareTag("Obstical"))
                    Destroy(child.gameObject);
            }
        }

        transform.position = new Vector3(0,0, transform.position.z);
        transform.localScale = Vector3.one;

        GetComponent<PlayerDeath>().Continue();
    }
}
