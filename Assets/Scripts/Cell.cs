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

// [ExecuteInEditMode]
public class Cell : MonoBehaviour
{
    public CellType type;
    
    [Header("Urban")]
    [SerializeField] private Material urbanColor;
    [SerializeField] private List<GameObject> urbanPrefabs;
    [SerializeField] private float urbanPerTick;
    [SerializeField] private float urbanPollutionPerTick;
    [SerializeField] private float urbanCooldown;
    
    [Header("Wind")]
    [SerializeField] private Material windColor;
    [SerializeField] private float windCooldown;
    [SerializeField] private float windMoneyPerTick;
    [SerializeField] private GameObject windmillPrefab;
    [SerializeField] private GameObject windPrefab;
    [SerializeField] private AudioClip windmillBuildingSound;

    [Header("Coal")]
    [SerializeField] private Material coalColor;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private List<GameObject> coalPrefabs;
    [SerializeField] private AudioClip mineBuildingSound;
    [SerializeField] private float coal;
    [SerializeField] private float coalCooldown;
    [SerializeField] private float coalMoneyPerTick;
    [SerializeField] private float pollutionPerTick;
    public float CoalAmount => coal;

    [Header("Environment")] 
    [SerializeField] private Material waterColor;
    [SerializeField] private Material forestColor;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private List<GameObject> forestPrefabs;

    [Header("Default")] 
    [SerializeField] private Transform defaultPrefab;
    [SerializeField] private Material defaultColor;
    [SerializeField] public float altitude = 0;
    [SerializeField] private Transform spawnPoint;

    [Header("Stats and Events")] 
    [SerializeField] private GameStats stats;
    [SerializeField] private GameEvent PollutionIncreased;
    [SerializeField] private GameEvent MoneyChanged;
    [SerializeField] private GameObject buildParticles;
    
    [SerializeField] private bool isBuiltOn = false;
    public bool IsBuiltOn => isBuiltOn;
    private float nextTick = 0f;
    private GameObject topObject;
    private AudioSource _audioSource;
    private Transform _transform;


    private void Start()
    {
        UpdateTile();
        _audioSource = GetComponent<AudioSource>();
        _transform = transform;
    }

    public void UpdateTile()
    {
         if (topObject != null) 
             StartCoroutine(DestroyGameObject(topObject.gameObject));
         
         float scale = Random.Range(0.5f, 1f);
         Vector3 localScaleVect = new Vector3(scale, scale, scale);
        
         float height = Mathf.Max(0, altitude);
         transform.localScale = new Vector3(1f, height + 0.2f, 1f);
         Vector3 pos = new Vector3(transform.position.x, 0.2f + height / 2, transform.position.z);
         transform.localPosition = pos;
         
        switch (type)
        {
            case CellType.Urban:
                GetComponent<MeshRenderer>().material = urbanColor;
                topObject = Instantiate(urbanPrefabs[Random.Range(0, urbanPrefabs.Count)], spawnPoint.position, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                topObject.transform.localScale = localScaleVect;
                topObject.transform.parent = transform;
                break;
            case CellType.Wind:
                if (isBuiltOn)
                {
                    topObject = Instantiate(windmillPrefab, spawnPoint.position,
                        Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                    topObject.transform.localScale = localScaleVect;
                    topObject.transform.parent = transform;
                }
                GetComponent<MeshRenderer>().material = windColor;
                break;
            case CellType.Water:
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
    }
    
    public void SetTileHeight(float heightToSet) {
        
                Debug.Log(heightToSet);
             float height = Mathf.Max(0, heightToSet);
             transform.localScale = new Vector3(1f, height + 0.2f, 1f);
             Vector3 pos = new Vector3(transform.position.x, 0.2f + height / 2, transform.position.z);
             transform.localPosition = pos;
    }

    // public void StartRising(float level)
    // {
    //     StartCoroutine(AnimateWaterRising(level));
    // }

    // private IEnumerator AnimateWaterRising(float level)
    // {
    //     float initialAltitude = altitude;
    //     float timeElapsed = 0;
    //     while (timeElapsed <= waterRiseTime)
    //     {
    //         float height = Mathf.Lerp(initialAltitude, level, timeElapsed / waterRiseTime);
    //         SetTileHeight(height);
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
    // }

    private void OnValidate()
    {
       UpdateTile();

    }

    private void Update()
    {
        
        if (isBuiltOn)
        {
            switch (type)
            {
                case CellType.Coal:
                    if (Time.time >= nextTick && coal > 0)
                    {
                        stats.money += coalMoneyPerTick;
                        stats.pollution += pollutionPerTick;
                        coal -= coalMoneyPerTick;
                        nextTick = Time.time + coalCooldown / stats.timeScale;
                        PollutionIncreased.Raise();
                        MoneyChanged.Raise();

                    }
                    break;
                case CellType.Wind:
                    if (Time.time >= nextTick)
                    {
                        stats.money += windMoneyPerTick;
                        nextTick = Time.time + windCooldown / stats.timeScale;
                        MoneyChanged.Raise();
                    }
                    break;
                case CellType.Default:
                    break;
            }
        }

        if (type == CellType.Urban && Time.time >= nextTick)
        {

                stats.money += urbanPerTick;
                stats.pollution += urbanPollutionPerTick;
                nextTick = Time.time + urbanCooldown / stats.timeScale;
                PollutionIncreased.Raise();
                MoneyChanged.Raise();
            
        }
    }

    private IEnumerator DestroyGameObject(GameObject gameObject) 
    {
        yield return new WaitForSeconds(0);
        DestroyImmediate(gameObject);
    }

    public void Build()
    {
        if (type == CellType.Coal)
        {
            isBuiltOn = true;
            if (topObject != null)
            {
                Destroy(topObject.gameObject);
            }
            topObject = Instantiate(minePrefab, spawnPoint.position, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            topObject.transform.parent = transform;
            _audioSource.PlayOneShot(mineBuildingSound);
            
        }
        
        if (type == CellType.Wind)
        {
            isBuiltOn = true;
            if (topObject != null)
            {
                Destroy(topObject.gameObject);
            }
            topObject = Instantiate(windmillPrefab, spawnPoint.position, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            topObject.transform.parent = transform;
            _audioSource.PlayOneShot(windmillBuildingSound);

        }

        Instantiate(buildParticles, spawnPoint.position, Quaternion.identity);

    }
}
