using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Stats", menuName = "Stats/Game Stats")]
public class GameStats : ScriptableObject
{
    public float pollution = 0f;
    public float money = 0;
    public int seaLevel = 0;
    
    public void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        pollution = 0;
        money = 0;
        seaLevel = 0;
    }
}
