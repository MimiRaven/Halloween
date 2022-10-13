using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PossessLight : MonoBehaviour
{
    Light2D lightScript;
    public GameObject scareRadius;
    void Start()
    {
        lightScript = GetComponent<Light2D>();
    }

    void Update()
    {

    }

    public void FlickerLightOn()
    {
        lightScript.intensity = 0;
    }

    public void FlickerLightOff()
    {
        lightScript.intensity = 1;
    }
    
}
