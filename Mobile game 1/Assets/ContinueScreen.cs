using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
            StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
