using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameStats stats;
    [SerializeField] private Camera _camera;
    
    [Header("Very hacky UI")]
    [SerializeField] private GameObject buildingUI;
    [SerializeField] private Canvas buildingCanvas;
    [SerializeField] private TextMeshProUGUI popupTitle;
    [SerializeField] private TextMeshProUGUI buildingPrice;
    [SerializeField] private TextMeshProUGUI amountRemaining;
    [SerializeField] private GameObject buildButton;
    [SerializeField] private GameEvent MoneyChanged;

    private Cell selectedCell;
    private AudioSource _audioSource;
    public Cell SelectedCell => selectedCell;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stats.money += 100;
        }

        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
            {
                Cell cell = hit.transform.GetComponent<Cell>();
                if (cell.type == CellType.Coal || cell.type == CellType.Wind)
                {
                    if (cell != selectedCell)
                    {
                        selectedCell = cell;
                        OpenBuildingUI();
                    }

                } else 
                    DismissBuildingUI();
            }
            else
            {
                DismissBuildingUI();
            }

        }

    }
    
    

    private void OpenBuildingUI()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        buildingCanvas.gameObject.SetActive(true);
        if (selectedCell.type == CellType.Coal)
        {
            amountRemaining.enabled = true;
            if (selectedCell.IsBuiltOn)
            {
                popupTitle.SetText("Mine");
                buildButton.SetActive(false);
                buildingPrice.enabled = false;
            }
            else
            {
                buildButton.SetActive(true);
                buildingPrice.enabled = true;
                popupTitle.SetText("Coal");
                buildingPrice.SetText("Price: " + stats.minePrice);
            }
            amountRemaining.SetText("Coal: " + selectedCell.CoalAmount);
        }
        
        else if (selectedCell.type == CellType.Wind)
        {
            if (selectedCell.IsBuiltOn)
            {
                popupTitle.SetText("Windmill");
                buildButton.SetActive(false);
                buildingPrice.enabled = false;
                
            }
            else
            {
                buildButton.SetActive(true);
                buildingPrice.enabled = true;
                popupTitle.SetText("Wind");
                buildingPrice.SetText("Price: " + stats.windmillPrice);

            }

            amountRemaining.enabled = false;
        }

        Vector3 pos = Input.mousePosition + new Vector3(60, 0);
        pos.z = 0;
        buildingUI.transform.position = pos;
        

    }

    public void BuildOnSelectedCell()
    {
        stats.money -= (selectedCell.type == CellType.Coal) ? stats.minePrice : stats.windmillPrice; 
        selectedCell.Build();
        MoneyChanged.Raise();
        DismissBuildingUI();
    }

    private void DismissBuildingUI()
    {
        selectedCell = null;
        buildingCanvas.gameObject.SetActive(false);
    }
    
    
}
