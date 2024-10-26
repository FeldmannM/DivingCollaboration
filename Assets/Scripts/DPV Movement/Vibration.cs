using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Vibration : MonoBehaviour
{
    public bool activeVib = false;
    private float amplitude = 0f;
    [SerializeField]
    private GameObject lConG;
    [SerializeField]
    private GameObject rConG;
    private XRController lCon;
    private XRController rCon;
    private bool started;



    // Start is called before the first frame update
    void Start()
    {
        lCon = lConG.GetComponent<XRController>();
        rCon = rConG.GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeVib)
        {
            startVib();
            started = true;
            
        }
        else if(started)
        {
            stopVib();
            started = false;
        }
    }

    public void setAmplitude(float a)
    {
        amplitude = a;
    }
    public void startVib()
    {
        lCon.inputDevice.SendHapticImpulse(1, amplitude, 1.0f);
        rCon.inputDevice.SendHapticImpulse(1, amplitude, 1.0f);
    }
    public void stopVib()
    {
        lCon.inputDevice.StopHaptics();
        rCon.inputDevice.StopHaptics();
    }
}
