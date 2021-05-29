using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum CellType {
    Urban,
    Wind,
    Coal,
    Water,
    Default
}
public class Cell : MonoBehaviour
{
    public CellType type;
    
    [Header("Urban")]
    [SerializeField] private Material urbanColor;
    [SerializeField] private List<GameObject> urbanPrefabs;
    
    [Header("Wind")]
    [SerializeField] private Material windColor;
    [SerializeField] private float windCooldown;
    
    [Header("Coal")]
    [SerializeField] private Material coalColor;
    [SerializeField] private float coalCooldown;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private float coal;

    [Header("Environment")] 
    [SerializeField] private Material waterColor;
    [SerializeField] private Material forestColor;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private List<GameObject> forestPrefabs;

    [Header("Default")] 
    [SerializeField] private Transform defaultPrefab;
    [SerializeField] private Material defaultColor;
    [SerializeField] private float altitude = 0;
    [SerializeField] private Transform spawnPoint;
    
    private bool isBuiltOn = false;
    private float nextTick = 0f;


    private void OnValidate()
    {
        switch (type)
        {
            case CellType.Urban:
                GetComponent<MeshRenderer>().material = urbanColor;
                break;
            case CellType.Wind:
                GetComponent<MeshRenderer>().material = windColor;
                break;
            case CellType.Water:
                GetComponent<MeshRenderer>().material = waterColor;
                break;
            case CellType.Coal:
                GetComponent<MeshRenderer>().material = coalColor;
                break;
            default:
                GetComponent<MeshRenderer>().material = defaultColor;
                break;
        }

        transform.localScale = new Vector3(1f, Mathf.Max(0.2f, altitude), 1f);
        Vector3 pos = new Vector3(transform.position.x, Mathf.Max(0f,(altitude - 0.1f - transform.localScale.y / 2f)), transform.position.z);
        transform.localPosition = pos;

    }

    private void Update()
    {
        
        if (isBuiltOn)
        {
            switch (type)
            {
                case CellType.Coal:
                    if (Time.time >= nextTick)
                    {
                        nextTick =  Time.time + coalCooldown;
                    }
                    break;
                case CellType.Wind:
                    if (Time.time >= nextTick)
                    {
                        nextTick = Time.time + windCooldown;
                    }
                    break;
                case CellType.Default:
                    break;
            }
        }
    }

    public void Build()
    {
        
    }
}
