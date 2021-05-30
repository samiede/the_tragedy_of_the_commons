using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private Cell cellPrefab;

    private Coord mapCenter;
    private List<Cell> allCells;
    public List<Cell> AlLCell => allCells;
    
    public void GenerateMap()
    {
        allCells = new List<Cell>();
        mapCenter = new Coord((int) mapSize.x / 2,(int) mapSize.y / 2);
        
        Vector3 tileScale = cellPrefab.transform.localScale;
        
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                    Vector3 tilePosition = CoordToPosition(x, y, tileScale);
                    Cell newTile = Instantiate(cellPrefab, tilePosition, Quaternion.identity, mapHolder);
                    newTile.type = CellType.Default;
                    allCells.Add(newTile);
            }
        }
    }
    
    Vector3 CoordToPosition(int x, int y, Vector3 scale)
    {
        return new Vector3((-mapSize.x / 2 + 0.5f + x) * scale.x,
            -scale.y / 2,
            (-mapSize.y / 2 + 0.5f + y) * scale.x);
    }
    
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Coord coord)
            {
                return this == coord;
            }

            return false;
        }

        public override int GetHashCode() => new { x, y }.GetHashCode();
    }

    
    
}
