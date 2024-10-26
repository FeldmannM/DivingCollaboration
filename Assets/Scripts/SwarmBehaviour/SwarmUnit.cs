using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SwarmUnit : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask cameraMask;
    [SerializeField] private Vector3[] directionsToCheckWhenAvoidingObstacles;
    
    private float _speed;
    private Vector3 _currentObstacleAvoidanceVector;
    private Vector3 _currentVelocity;

    private static bool[,] _distances;
    private int id = 0;
    public int ID
    {
        get => id;
        set => id = value;
    }


    private PostProcessGray _processGray;
    private float _gameOverTime;
    private float _lifeTime;
    public float LifeTime
    {
        get => _lifeTime;
        set => _lifeTime = value;
    }

    private GameObject _ground;

    // Start is called before the first frame update
    void Start()
    {
        _processGray = GameObject.Find("Grayscale").GetComponent<PostProcessGray>();
        _speed = Random.Range(SwarmManager.SM.MinSpeed, SwarmManager.SM.MaxSpeed);

        _distances = new bool[SwarmManager.SM.AllFish.Length, SwarmManager.SM.AllFish.Length];

    }

    // Update is called once per frame
    void Update()
    {
        // The first fish should be the last to deactivate
        if (id == 0 && _processGray.aliveCorals <= 0)
        {
            this.transform.gameObject.SetActive(false);
        }
        if (id != 0 && _processGray.aliveCorals <= _lifeTime)
        {
            this.transform.gameObject.SetActive(false);
        }
        else
        {
            float random = Random.Range(0, 100);
            if (random < 5)
            {
                _speed = Random.Range(SwarmManager.SM.MinSpeed, SwarmManager.SM.MaxSpeed);
            }
            else if (random < 20)
            {
                ApplyRules();
            }
            
            
            if (id == 0 && random < 20)
            {
                CalculateDistances();
            }
            
        
            Vector3 obstacleVector = CalculateObstacleVector() * 10;
        
            Vector3 moveVector = obstacleVector;
			
			
            moveVector = Vector3.SmoothDamp(transform.forward, moveVector, ref _currentVelocity, 1);
            moveVector = moveVector.normalized * _speed;
            if (moveVector == Vector3.zero)
                moveVector = transform.forward;

            transform.forward = moveVector;
            transform.position += moveVector * Time.deltaTime;
        }
    }

    private static void CalculateDistances()
    {
        foreach (SwarmUnit firstUnit in SwarmManager.SM.AllFish)
        {
            int firstUnitID = firstUnit.id;
            foreach (SwarmUnit secondUnit in SwarmManager.SM.AllFish)
            {
                int secondUnitID = secondUnit.id;

                if (firstUnitID != secondUnitID)
                {
                    bool closeUnits = Vector3.Distance(firstUnit.transform.position, secondUnit.transform.position) <
                                      1.0f;

                    _distances[firstUnitID, secondUnitID] = closeUnits;
                    _distances[secondUnitID, firstUnitID] = closeUnits;
                }
            }
        }
    }

    private Vector3 CalculateObstacleVector()
    {
        Vector3 obstacleVector = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2,
                obstacleMask) || Physics.Raycast(transform.position, transform.forward, out hit, 8,
                cameraMask))
        {
            obstacleVector = FindBestDirectionToAvoidObstacle();
        }
        else
        {
            _currentObstacleAvoidanceVector = Vector3.zero;
        }

        return obstacleVector;
    }

    private Vector3 FindBestDirectionToAvoidObstacle()
    {
        if (_currentObstacleAvoidanceVector != Vector3.zero)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.forward, out hit, 2,
                    obstacleMask) || !Physics.Raycast(transform.position, transform.forward, out hit, 8,
                    cameraMask))
            {
                return _currentObstacleAvoidanceVector;
            }
        }

        float maxDistance = int.MinValue;
        Vector3 selectedDirection = Vector3.zero;
        for (int i = 0; i < directionsToCheckWhenAvoidingObstacles.Length; i++)
        {

            RaycastHit hit;
            Vector3 currentDirection =
                transform.TransformDirection(directionsToCheckWhenAvoidingObstacles[i].normalized);
            if (Physics.Raycast(transform.position, currentDirection, out hit, 2,
                    obstacleMask) || Physics.Raycast(transform.position, currentDirection, out hit, 8,
                    cameraMask))
            {

                float currentDistance = (hit.point - transform.position).sqrMagnitude;
                if (currentDistance > maxDistance)
                {
                    maxDistance = currentDistance;
                    selectedDirection = currentDirection;
                }
            }
            else
            {
                selectedDirection = currentDirection;
                _currentObstacleAvoidanceVector = currentDirection.normalized;
                return selectedDirection.normalized;
            }
        }

        return selectedDirection.normalized;
    }

    private void ApplyRules()
    {
        SwarmUnit[] gos;
        gos = SwarmManager.SM.AllFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        int groupSize = 0;

        foreach (SwarmUnit go in gos)
        {
            if (go.gameObject.activeSelf && go.gameObject != this.gameObject)
            {
                vcentre += go.transform.position;
                groupSize++;

                if (_distances[this.id, go.id])
                {
                    vavoid += (this.transform.position - go.transform.position);
                }

                gSpeed += go._speed;
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (SwarmManager.SM.GoalPos - this.transform.position);
            _speed = gSpeed / groupSize;

            if (_speed > SwarmManager.SM.MaxSpeed)
            {
                _speed = SwarmManager.SM.MaxSpeed;
            }

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), SwarmManager.SM.RotationSpeed * Time.deltaTime);
            }
        }

    }
}
