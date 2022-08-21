using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    protected GameObject Player; 
    public float TimeLasted = 1f;
    public bool Active = false;

    public virtual void Activate()
    {
        Active = true;
    }

    public virtual void Deactivate()
    {
        Active = false;
    }

    public bool IsActive()
    {
        return Active;
    }

    private void Update()
    {
        if (TimeLasted <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
