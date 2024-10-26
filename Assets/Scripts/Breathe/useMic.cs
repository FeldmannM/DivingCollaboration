using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useMic : MonoBehaviour
{
    [SerializeField]
    private int window = 64;
    private float loudness = 0f;
    private AudioClip micAudio;
    // Start is called before the first frame update
    void Start()
    {
        MicToAudio();
    }
    
    public void MicToAudio()
    {
        string micName = Microphone.devices[0];
        Debug.Log(micName);
        micAudio = Microphone.Start(micName, true, 10, AudioSettings.outputSampleRate);
    }
    public float getLoudness()
    {
        int startPos = Microphone.GetPosition(Microphone.devices[0]) - window;
        if(startPos < 0)
        {
            return 0f;
        }
        float[] data = new float[window];
        micAudio.GetData(data, startPos);

        float totalL = 0;
        for (int i = 0; i < window; i++)
        {
            // full positive Curve
            totalL += Mathf.Abs(data[i]);
        }
        loudness = totalL / window;
        return loudness;
    }
}
