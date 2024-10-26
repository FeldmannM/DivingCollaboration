using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class fallWaste : MonoBehaviour
{
    public float speed = 1f;
    RaycastHit _hit;
    Ray _downRay;
    Vector3 _target;
    private Vector3 _lastPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        float x = this.gameObject.GetComponent<BoxCollider>().bounds.size.x;
        float y = this.gameObject.GetComponent<BoxCollider>().bounds.size.y;
        float z = this.gameObject.GetComponent<BoxCollider>().bounds.size.z;
        float max = Mathf.Max(x,y,z);
        
        
        _downRay = new Ray(transform.position, -Vector3.up);
        Physics.Raycast(_downRay, out _hit);
        _target = new Vector3(_hit.point.x, _hit.point.y + max + 0.5f, _hit.point.z);
        transform.rotation = Random.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        _lastPosition = transform.position;
        if (Vector3.Distance(transform.position, _target) > 0){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target, step);
        }

        // If there is no difference, then disable this script
        if (_lastPosition == transform.position)
        {
            this.enabled = false;
        }
            
    }
}
