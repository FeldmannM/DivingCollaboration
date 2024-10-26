using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform lineTransform;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float distance; 
    [SerializeField] private bool usePlayerDirection;
    [SerializeField] private Transform playerLocomotion; 
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject spawnWasteParent;
    [SerializeField] private GameObject dotsParent;

    private const string WasteTag = "Waste";
    private const string DotTag = "WasteDot";

    void Update()
    {
        lineTransform.eulerAngles -= new Vector3(0, 0, -rotationSpeed * Time.deltaTime);

        RemoveAllDots();
        CalculateDots();
    }

    private void CalculateDots()
    {
        Vector3 playerPos = playerLocomotion.position;
        foreach (Transform target in spawnWasteParent.transform)
        {
            Vector3 targetPos = target.transform.position;
            float distanceToTarget = Vector3.Distance(targetPos, playerPos);

            if (distanceToTarget <= distance)
            {
                Vector3 normalisedTargetPosition = NormalisedPosition(playerPos, targetPos);
                Vector2 dotPosition = CalculatePosition(normalisedTargetPosition);
                ShowDot(dotPosition);
            }
        }
    }

    private void RemoveAllDots()
    {
        foreach (Transform dot in dotsParent.transform)
        {
            Destroy(dot.gameObject);
        }
    }

    private Vector3 NormalisedPosition(Vector3 playerPos, Vector3 targetPos)
    {
        float normalisedTargetX = (targetPos.x - playerPos.x) / (distance * 10);
        float normalisedTargetZ = (targetPos.z - playerPos.z) / (distance * 10);

        return new Vector3(normalisedTargetX, 0, normalisedTargetZ);
    }

    private Vector2 CalculatePosition(Vector3 targetPos)
    {
        float angleToTarget = Mathf.Atan2(targetPos.x, targetPos.z) * Mathf.Rad2Deg;
        float anglePlayer = usePlayerDirection ? playerLocomotion.eulerAngles.y : 0;
        float angleRadarDegrees = angleToTarget - anglePlayer - 90;

        float normalisedDistanceToTarget = targetPos.magnitude;
        float angleRadians = angleRadarDegrees * Mathf.Deg2Rad;
        float dotX = normalisedDistanceToTarget * Mathf.Cos(angleRadians);
        float dotY = normalisedDistanceToTarget * Mathf.Sin(angleRadians);

        return new Vector2(dotX, dotY);
    }

    private void ShowDot(Vector2 pos)
    {
        GameObject dot = Instantiate(dotPrefab, this.dotsParent.transform, false);
        dot.transform.localPosition = new Vector3(pos.x, pos.y, 0);
    }
}
