using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPosAfterAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject bone;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = bone.transform.localPosition;
    }

    

    void LateUpdate()
    {
        bone.transform.localPosition = originalPos;

    }
}
