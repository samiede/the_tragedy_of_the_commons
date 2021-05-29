using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CellType {
    Urban,
    Forrest,
    Agriculture,
    Unoccupied
}
public class Cell : MonoBehaviour
{
    public CellType type;
    [SerializeField] private Material urbanColor;
    [SerializeField] private Material forrestColor;
    [SerializeField] private Material agricultureColor;
    [SerializeField] private Material unoccupiedColor;
    [SerializeField] private float altitude;
    
    private void Start()
    {
    }

    private void OnValidate()
    {
        switch (type)
        {
            case CellType.Urban:
                GetComponent<MeshRenderer>().material = urbanColor;
                break;
            case CellType.Agriculture:
                GetComponent<MeshRenderer>().material = agricultureColor;
                break;
            case CellType.Forrest:
                GetComponent<MeshRenderer>().material = forrestColor;
                break;
            default:
                GetComponent<MeshRenderer>().material = unoccupiedColor;
                break;
        }

        Vector3 pos = new Vector3(transform.position.x, altitude, transform.position.z);
        transform.localPosition = pos;
    }
}
