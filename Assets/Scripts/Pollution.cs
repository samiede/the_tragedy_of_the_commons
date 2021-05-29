using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pollution : MonoBehaviour
{
    public Text pollutionText;
    //GameStats pollution;
    public GameStats stats;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        pollutionText.text = "Pollution: " + stats.pollution.ToString();
    }
}
