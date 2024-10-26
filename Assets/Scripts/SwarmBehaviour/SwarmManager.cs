using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    public static SwarmManager SM;
    public SwarmUnit[] AllFish { get => _allFish; }
    public Vector3 GoalPos => goalTransform.position;
    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;
    public float RotationSpeed => rotationSpeed;
    
    [Header("Settings")]
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject fishPrefab2;
    [SerializeField] private int numFish;
    [SerializeField] private Vector3 swimLimit;
    [SerializeField] private Transform goalTransform;
    
    [Header("Speed")] 
    [Range(0.0f, 5.0f)]
    [SerializeField] private float minSpeed;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float maxSpeed;
    [Range(1.0f, 5.0f)]
    [SerializeField] private float rotationSpeed;
    
    
    private SwarmUnit[] _allFish;
    private GameObject _fishTemp;
    
    
    // Start is called before the first frame update
    void Start()
    {

        var deathRate = new[] { (0, 1500), (1503, 3000), (3000, 5400), (5400, 7500) };
        _allFish = new SwarmUnit[numFish];
        for (int index = 0; index < numFish; index++)
        {
            Vector3 startPos = this.transform.position + new Vector3(
                Random.Range(-swimLimit.x, swimLimit.x),
                Random.Range(-swimLimit.y, swimLimit.y),
                Random.Range(-swimLimit.z, swimLimit.z)
            );
            
            if (Random.Range(0,100) < 60)
            {
                _fishTemp = fishPrefab;
            }
            else
            {
                _fishTemp = fishPrefab2;
            }
            
            GameObject fish = Instantiate(_fishTemp, startPos, Quaternion.identity);
            var (minDeath, maxDeath) = deathRate[index % 4];
            fish.GetComponent<SwarmUnit>().LifeTime = Random.Range(minDeath, maxDeath);
            _allFish[index] = fish.GetComponent<SwarmUnit>();
            _allFish[index].ID = index;
        }

        SM = this;
    }
}