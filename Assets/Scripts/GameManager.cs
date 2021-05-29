using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats.Reset();
        
    }
    
}
