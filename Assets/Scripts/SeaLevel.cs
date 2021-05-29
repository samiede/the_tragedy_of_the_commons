using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaLevel : MonoBehaviour
{
    public Text seaLevelText;
    public GameStats stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seaLevelText.text = "Sea Level: " + stats.seaLevel.ToString();
    }
}
