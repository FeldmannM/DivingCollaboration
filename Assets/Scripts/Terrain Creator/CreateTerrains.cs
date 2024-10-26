using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateTerrains : MonoBehaviour
{
    [SerializeField] private Transform locomotion;
    private const string TargetTag = "TerrainNeighbour"; // The tag to check for
    private List<Vector3> _givenCoordinateList = new List<Vector3>();
    [SerializeField] private List<Terrain> _neighbourTerrains;
    [SerializeField] private Terrain _terrainCopy;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = new Vector3(0, 0, 0);
        for (int index = 0; index < _neighbourTerrains.Count; index++)
        {
            _givenCoordinateList.Add(_neighbourTerrains[index].GetPosition());
        }
        
        // Call this method every second
        InvokeRepeating("DeterminePossibleTerrains", 0f, 1f);
    }

    private void DeterminePossibleTerrains()
    {
        // Raycast from the player's position downward
        RaycastHit hit;
        if (Physics.Raycast(locomotion.position, Vector3.down, out hit))
        {
            // Check if the hit object has the specified tag
            if (hit.collider.CompareTag(TargetTag))
            {
                Vector3 hitTerrainPosition = hit.collider.gameObject.transform.position;

                if (hitTerrainPosition != lastPosition)
                {
                    lastPosition = hitTerrainPosition;
                    
                    float xPos = hitTerrainPosition.x;
                    float zPos = hitTerrainPosition.z;

                    Vector3[] newPositions = new Vector3[8]
                    {
                        new Vector3(xPos+50, 0, zPos-0),
                        new Vector3(xPos-50, 0, zPos-0),
                        new Vector3(xPos+0, 0, zPos+50),
                        new Vector3(xPos-0, 0, zPos-50),
                        new Vector3(xPos+50, 0, zPos+50),
                        new Vector3(xPos+50, 0, zPos-50),
                        new Vector3(xPos-50, 0, zPos+50),
                        new Vector3(xPos-50, 0, zPos-50)
                    };

                    for (int i = 0; i < newPositions.Length; i++)
                    {
                        bool positionGiven = false;
                        foreach (Vector3 coordinates in _givenCoordinateList)
                        {
                            if (coordinates == newPositions[i])
                            {
                                positionGiven = true;
                                break;
                            }
                        }

                        if (!positionGiven)
                        {
                            CreateTerrain(newPositions[i]);
                        }
                    }
                }
                

            }
        }
    }

    private void CreateTerrain(Vector3 position)
    {
        Terrain newTerrain = Instantiate(_terrainCopy, this.transform);
        newTerrain.transform.position = position;
        _givenCoordinateList.Add(position);
        newTerrain.gameObject.SetActive(true);
    }
}
