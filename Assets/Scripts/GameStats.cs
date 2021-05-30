using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Stats", menuName = "Stats/Game Stats")]
public class GameStats : ScriptableObject
{

    [Header("Game Stats")]
    public float pollution = 0f;
    public float money = 0;
    public int seaLevel = 0;
    public int currentYear;
    public int timeScale = 1;
    
    [Header("Initial values")]
    public float initialPollution = 0f;
    public float initialMoney = 0;
    public int initialSeaLevel = 0;
    
    [Header("Building Prices")] 
    public int minePrice = 1000;
    public int windmillPrice = 10000;

    public void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        pollution = initialPollution;
        money = initialMoney;
        seaLevel = initialSeaLevel;
    }
}
