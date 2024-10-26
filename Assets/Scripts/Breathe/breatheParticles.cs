using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breatheParticles : MonoBehaviour
{
    [SerializeField]
    private useMic mic;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private AudioSource audioS;
    [SerializeField]
    private float freqTime;
    private float loudness;
    private float factor;
    private float passedTime = 0f;
    private float breatheCurve = 0f;
    private ParticleSystem.EmissionModule emission;
    // Start is called before the first frame update
    void Start()
    {
        emission = particles.emission;
        freqTime *= 2;
    }

    // Update is called once per frame
    void Update()
    {
        loudness = mic.getLoudness();
        //particles.Play();
        factor = loudness * 1500;
        breatheCurve = Mathf.Abs(Mathf.Sin((Mathf.PI / freqTime) * passedTime));
        if (breatheCurve > 0.9f)
        {
            breatheCurve -= 0.9f;
            factor = Mathf.Clamp(factor, 400 * breatheCurve, 40);
        }
        else
        {
            factor = Mathf.Clamp(factor, 0.01f, 40);
        }
        //Debug.Log("Breathe Factor: " + factor);
        emission.rateOverTime = factor;

        if (factor > 5 && !audioS.isPlaying)
        {
            audioS.time = 0.2f;
            audioS.Play();
        } else if(factor <= 5 && audioS.isPlaying)
        {
            audioS.Stop();
        }
        passedTime += Time.deltaTime;

        /*
        if (factor == 0)
        {
            particles.Stop();
            if (audioS.isPlaying)
            {
                audioS.Stop();
            }
        }*/
    }
}
