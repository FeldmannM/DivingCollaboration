using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class dpvMove : MonoBehaviour
{
    [SerializeField]
    private XRGrabInteractable grabO;
    [SerializeField]
    private GameObject locomotion;
    [SerializeField]
    private GameObject dpv;
    [SerializeField]
    private rotorMove rotor;
    [SerializeField]
    private GameObject vibration;
    private Vibration _vibration;
    [SerializeField]
    private InputActionProperty button;
    [SerializeField]
    private float maxSpeed = 10;
    [SerializeField]
    private ParticleSystem particles;
    private float orgPartSpeed;
    private float orgGravSpeed;
    // Start is called before the first frame update
    void Start()
    {
        orgPartSpeed = particles.startSpeed;
        orgGravSpeed = particles.gravityModifier;
        _vibration = vibration.GetComponent<Vibration>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabO.interactorsSelecting.Count >= 1 || Input.GetKey(KeyCode.W))
        {
            Vector3 direction = dpv.transform.forward;
            float bIntensity = button.action.ReadValue<float>();
            //bIntensity = 1;
            if(bIntensity > 0f)
            {
                _vibration.activeVib = true;
                _vibration.setAmplitude(bIntensity);
            }
            else
            {
                _vibration.activeVib = false;
                rotor.SetMovement(0f);
            }
            float speed = maxSpeed * bIntensity;
            rotor.SetMovement(speed);
            locomotion.transform.position += direction * (speed * Time.deltaTime);
            particles.startSpeed = orgPartSpeed + speed*speed;
            particles.gravityModifier = orgGravSpeed * (1 + speed*speed);
        }else if (_vibration.activeVib == true)
        {
            _vibration.activeVib = false;
            rotor.SetMovement(0f);
        }
    }
}
