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
    public TextMeshProUGUI currentYearText;
    //GameStats pollution;
    public GameStats stats;

    private void Start()
    {
        UpdatePollution();
        UpdateMoney();
        UpdateCurrentYear();
    }

    public void UpdatePollution()
    {
        pollutionText.SetText("Pollution: " + stats.pollution);
    }

    public void UpdateMoney()
    {
        moneyText.SetText("Money: " + stats.money);
    }

    public void UpdateCurrentYear()
    {
        currentYearText.SetText("Year: " + stats.currentYear);
    }
}
