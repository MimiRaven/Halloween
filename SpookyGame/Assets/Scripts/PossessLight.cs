using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PossessLight : MonoBehaviour
{
    Light2D lightScript;
    public GameObject scareRadius;
    public GameObject lightParticles;
    public AudioSource lightOn;
    public AudioSource lightOff;
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
        lightOn.Play();
    }

    public void FlickerLightOff()
    {
        lightScript.intensity = 1;
        lightOff.Play();
    }
    
}
