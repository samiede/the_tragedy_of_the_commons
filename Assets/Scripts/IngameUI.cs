using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IngameUI : MonoBehaviour
{
    public TextMeshProUGUI pollutionText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI seaLevelText;
    //GameStats pollution;
    public GameStats stats;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        pollutionText.SetText("Pollution: " + stats.pollution);
        moneyText.SetText("Money: " + stats.money);
        seaLevelText.SetText("Sea Level: " + stats.seaLevel);
    }
}
