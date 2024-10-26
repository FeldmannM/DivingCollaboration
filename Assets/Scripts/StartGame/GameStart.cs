using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStart : MonoBehaviour
{
    [Header("Scripts to enable")]
    [SerializeField] private spawnWaste spawnWaste;
    [SerializeField] private CoralManager coralManager;

    [Header("GameObjects to set active")] 
    [SerializeField] private GameObject dpv;
    [SerializeField] private GameObject radar;


    public void StartTheGame()
    {
        coralManager.enabled = true;
        spawnWaste.enabled = true;
        
        dpv.SetActive(true);
        radar.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
