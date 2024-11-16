using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class swimming : MonoBehaviour
{
    [SerializeField]
    private GameObject locomotion;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private float movementThreshold = 0.001f;
    [SerializeField]
    private InputActionReference leftConPos;
    [SerializeField]
    private InputActionReference rightConPos;

    private Vector3 lastLeftConPos;
    private Vector3 lastRightConPos;
    private Vector3 currentLeftConPos;
    private Vector3 currentRightConPos;

    // Start is called before the first frame update
    void Start()
    {
        if (leftConPos != null && rightConPos != null)
        {
            lastLeftConPos = leftConPos.action.ReadValue<Vector3>();
            lastRightConPos = rightConPos.action.ReadValue<Vector3>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(leftConPos != null && rightConPos != null)
        {
            currentLeftConPos = leftConPos.action.ReadValue<Vector3>();
            currentRightConPos = rightConPos.action.ReadValue<Vector3>();

            if(currentLeftConPos.z < lastLeftConPos.z - movementThreshold && currentRightConPos.z < lastRightConPos.z - movementThreshold)
            {
                Vector3 forwardDirection = mainCamera.transform.forward;
                Vector3 movement = forwardDirection * speed * Time.deltaTime;
                locomotion.transform.position += movement;
            }

            lastLeftConPos = currentLeftConPos;
            lastRightConPos = currentRightConPos;
        }
    }
}
