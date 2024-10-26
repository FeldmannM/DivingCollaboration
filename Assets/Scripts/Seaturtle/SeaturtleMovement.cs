using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaturtleMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject turtle;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float turnspeed = 20f;

    [SerializeField] private int lifeTime;
    
    private readonly float _forwardTime = 10f;
    private float _angle = 0f;
    private float _fullAngle = 0f;
    private bool _turnRight = false;
    private bool _pause = true;
    private PostProcessGray _processGray;
    void Start()
    {
        _processGray = GameObject.Find("Grayscale").GetComponent<PostProcessGray>();
        StartCoroutine(ForwardMoveStart());
    }

    private void Update()
    {
        if (_processGray.aliveCorals <= lifeTime)
        {
            this.gameObject.SetActive(false);
            return;
        }
        
        turtle.transform.position += turtle.transform.right * (speed * Time.deltaTime);
        if (_pause)
        {
            _angle = 0f;
            return;
        }
        _angle = turnspeed * speed * Time.deltaTime;

        if (_turnRight)
        {
            turtle.transform.Rotate(Vector3.up, _angle);
            //turtle.transform.rotation = Quaternion.RotateTowards(turtle.transform.rotation, Quaternion.Euler(0, 180, 0), angle);
        }
        else
        {
            turtle.transform.Rotate(Vector3.up, -_angle);
            //turtle.transform.rotation = Quaternion.RotateTowards(turtle.transform.rotation, Quaternion.Euler(0, -180, 0), angle);
        }

        _fullAngle += _angle;
        //Debug.Log("fullangle:"+fullAngle);
        if (_fullAngle >= 180.0f)
        {
            //Debug.Log("Switch");
            _fullAngle = 0f;
            _pause = true;
            StartCoroutine(ForwardMove());
        }

    }
    IEnumerator ForwardMoveStart()
    {
        yield return new WaitForSeconds(_forwardTime);
        _pause = false;
        _turnRight = !_turnRight;
    }
    IEnumerator ForwardMove()
    {
        yield return new WaitForSeconds(2*_forwardTime);
        _pause = false;
        _turnRight = !_turnRight;
    }
}
