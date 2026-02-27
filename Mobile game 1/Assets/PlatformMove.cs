using NUnit.Framework.Constraints;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float TEMPspeed = 1;
    Rigidbody RB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.linearVelocity = new Vector3(0, 0, -TEMPspeed * Time.deltaTime);
    }
}
