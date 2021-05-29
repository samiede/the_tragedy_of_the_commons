using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameStats stats;
    [SerializeField] private Camera _camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stats.money += 100;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
            {
                Cell cell = hit.transform.GetComponent<Cell>();
                if (cell)
                {
                    cell.Build();
                }
            }

        }
    }
}
