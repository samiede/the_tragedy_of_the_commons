using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonUpdater : MonoBehaviour
{
    [SerializeField] private GameStats stats;
    [SerializeField] private PlayerController _controller;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        CheckEnabled();

    }

    public void CheckEnabled()
    {
        if (_controller.SelectedCell != null)
        {
            if (_controller.SelectedCell.type == CellType.Coal)
            {
                if (stats.money >= stats.minePrice)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
        }
        
        if (_controller.SelectedCell.type == CellType.Wind)
        {
            if (stats.money >= stats.windmillPrice)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }
}
