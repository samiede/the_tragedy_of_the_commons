using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private GameStats stats;
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
        

        int[,] mapCoal = new int[mapSize.x*2,mapSize.y]; 
        string m_Path = Application.dataPath + "/map_island3.csv";
        using(var reader = new StreamReader(@m_Path))
        {
            int i = 0;
            while (!reader.EndOfStream)
            {
                
                var line = reader.ReadLine();
                var values = line.Split(',');
                
                int j = 0;
                foreach (var val in values)
                {
                    Debug.Log(val);
                    mapCoal[i, j] = Int32.Parse(val);
                    j += 1;
                }
                i += 1;
            }
        }

        
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                    Vector3 tilePosition = CoordToPosition(x, y, tileScale);
                    Cell newTile = Instantiate(cellPrefab, tilePosition, Quaternion.identity, mapHolder);
                    
                    newTile.altitude =  (float) mapCoal[x+mapSize.x, y] /4;

                    if (mapCoal[x, y] == 1)
                    {
                        newTile.type = CellType.Urban;
                    }
                    else if (mapCoal[x, y] == 2)
                    {
                        newTile.type = CellType.Wind;
                    }
                    else if (mapCoal[x, y] == 3)
                    {
                        newTile.type = CellType.Coal;
                    }
                    else if (mapCoal[x, y] == 4)
                    {
                        newTile.type = CellType.Water;
                        newTile.altitude = 0;
                    }
                    else if (mapCoal[x, y] == 5)
                    {
                        newTile.type = CellType.Forest;
                    }
                    else if (mapCoal[x, y] == 0)
                    {
                        newTile.type = CellType.Default;                        
                    }

                    
                    
                    
                    
                    
                
                    allCells.Add(newTile);
            }
        }
    }

    private void Start()
    {
        GenerateMap();
    }

    public void SetSeaLevel()
    {
        foreach (Cell cell in allCells)
        {
            if (cell.altitude <= (float) stats.seaLevel / 4)
            {
                cell.type = CellType.Water;
                cell.altitude = (float) stats.seaLevel / 4;
                cell.UpdateTile();
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
