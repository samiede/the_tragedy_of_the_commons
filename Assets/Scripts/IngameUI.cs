using System;
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

    private void Start()
    {
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        pollutionText.SetText("Pollution: " + stats.pollution);
        moneyText.SetText("Money: " + stats.money);
        seaLevelText.SetText("Sea Level: " + stats.seaLevel);
    }
}
