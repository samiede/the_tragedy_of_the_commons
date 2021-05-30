using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameStats stats;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject buildingUI;
    [SerializeField] private Canvas buildingCanvas;

    // Update is called once per frame
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
                    OpenBuildingUI(Input.mousePosition);
                } else 
                    DismissBuildingUI();
            }
            else
            {
                DismissBuildingUI();
            }

        }
    }

    private void OpenBuildingUI(Vector3 position)
    {
        buildingCanvas.gameObject.SetActive(true);
        
        // Offset position above object bbox (in world space)
        float offsetPosX = position.x + 1.5f;
        float offsetPosY = position.y + 1.5f;
        // Final position of marker above GO in world space
        Vector3 offsetPos = new Vector3(offsetPosX, offsetPosY, position.z);
 
        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = _camera.WorldToScreenPoint(offsetPos);
 
        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(buildingCanvas.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);
 
        // Set
        buildingUI.transform.localPosition = canvasPos;
        

    }

    private void DismissBuildingUI()
    {
        buildingCanvas.gameObject.SetActive(false);
    }
    
    
}
