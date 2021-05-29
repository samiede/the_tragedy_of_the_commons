using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;


public enum CellType {
    Urban,
    Wind,
    Coal,
    Water,
    Forest,
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
    [SerializeField] private List<GameObject> coalPrefabs;
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
    
    [SerializeField] private bool isBuiltOn = false;
    private float nextTick = 0f;
    private GameObject topObject;


    private void OnValidate()
    {
        if (topObject != null)
        {
            StartCoroutine(DestroyGameObject(topObject.gameObject));
        }
        float scale = Random.Range(0.5f, 1f);
        Vector3 localScaleVect = new Vector3(scale, scale, scale);
        
        switch (type)
        {
            case CellType.Urban:
                GetComponent<MeshRenderer>().material = urbanColor;
                break;
            case CellType.Wind:
                GetComponent<MeshRenderer>().material = windColor;
                break;
            case CellType.Water:
                topObject = Instantiate(waterPrefab, spawnPoint.position, Quaternion.identity);
                topObject.transform.parent = transform;
                GetComponent<MeshRenderer>().material = waterColor;
                break;
            case CellType.Coal:
                if (!isBuiltOn)
                    topObject = Instantiate(coalPrefabs[Random.Range(0, coalPrefabs.Count)], spawnPoint.position, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                else
                    topObject = Instantiate(minePrefab, spawnPoint.position, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                topObject.transform.parent = transform;
                GetComponent<MeshRenderer>().material = coalColor;
                break;            
            case CellType.Forest:
                GetComponent<MeshRenderer>().material = forestColor;
                topObject = Instantiate(forestPrefabs[Random.Range(0, forestPrefabs.Count)], spawnPoint.position, Quaternion.identity);
                topObject.transform.localScale = localScaleVect;
                topObject.transform.parent = transform;
                break;
            default:
                GetComponent<MeshRenderer>().material = defaultColor;
                break;
        }

        transform.localScale = new Vector3(1f, Mathf.Max(0.2f, altitude), 1f);
        Vector3 pos = new Vector3(transform.position.x, Mathf.Max(0f,(altitude - 0.2f - transform.localScale.y / 2)), transform.position.z);
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

    private IEnumerator DestroyGameObject(GameObject gameObject) 
    {
        yield return new WaitForSeconds(0);
        DestroyImmediate(gameObject);
    }

    public void Build()
    {
        
    }
}
