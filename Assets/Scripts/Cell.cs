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
    
}
