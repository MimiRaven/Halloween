using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PossessLight : MonoBehaviour
{
    Light2D lightScript;
    public GameObject scareRadius;
    public GameObject lightParticles;
    void Start()
    {
        lightParticles.SetActive(false);
        lightScript = GetComponent<Light2D>();
    }

    void Update()
    {

    }

    public void ParticlesOn()
    {
        lightParticles.SetActive(true);
    }

    public void ParticlesOff()
    {
        lightParticles.SetActive(false);
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
