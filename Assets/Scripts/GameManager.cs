using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStats stats;
    [SerializeField] private GameEvent SeaLevelRising;
    [SerializeField] private GameEvent YearIncreased;
    [SerializeField] private int startYear;
    [SerializeField] private int endYear;
    [SerializeField] private int secondsPerYear;
    
    private bool isRunning;
    private WaitForSeconds yearTime;
    
    
    // [SerializeField] private int seaLevel;
    void Start()
    {
        stats.Reset();
        isRunning = true;
        stats.currentYear = startYear;
        YearIncreased.Raise();
        StartCoroutine(CountUpYears());
    }


    IEnumerator CountUpYears()
    {
        int currentYear = startYear;
        while (isRunning)
        {
            yield return new WaitForSeconds((float) secondsPerYear / stats.timeScale);;
            currentYear++;
            stats.currentYear = currentYear;
            YearIncreased.Raise();
            if (currentYear == endYear)
            {
                isRunning = false;
                Debug.Log("You Won");
            }
        }

    }
    

    public void CheckSeaLevel()
    {
        int tempSeaLevel = 0;
        if (stats.pollution > 0 && stats.pollution < 200)
        {
            tempSeaLevel = 0;
        }

        if (stats.pollution >= 200 && stats.pollution < 400)
        {
            tempSeaLevel = 1;
        } 
        if (stats.pollution >= 400 && stats.pollution < 600)
        {
            tempSeaLevel = 2;
        } 
        if (stats.pollution >= 600 && stats.pollution < 800)
        {
            tempSeaLevel = 3;
        }         
        if (stats.pollution >= 800 && stats.pollution < 1000)
        {
            tempSeaLevel = 4;
        }         
        if (stats.pollution >= 1000)
        {
            tempSeaLevel = 5;
        }

        if (tempSeaLevel > stats.seaLevel)
        {
            stats.seaLevel = tempSeaLevel;
            SeaLevelRising.Raise();
            
        }

        if (tempSeaLevel == 5)
        {
            isRunning = false;
            Debug.Log("Yoi lopse lol");
        }
    }
    
    
    
    
    
}
