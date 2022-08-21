using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RandomColour : MonoBehaviour
{
    public PostProcessVolume PP;
    public ColorGrading CG;

    private void Start()
    {
        PP = gameObject.GetComponent<PostProcessVolume>();
        PP.profile.TryGetSettings(out CG);

        CG.hueShift.value = Random.Range(-180, 180);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
