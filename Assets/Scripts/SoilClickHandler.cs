using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoilClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject seedsPanel;
    [SerializeField] private BuildingManager building;

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (building.isInBuildingMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            CheckFieldsUnderMouse();
        }
    }

    private void CheckFieldsUnderMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.TryGetComponent<Soil>(out var soil))
        {
            SeedsPanelHandler(soil);
            return;
        }

        seedsPanel.SetActive(false);
    }

    private void SeedsPanelHandler(Soil soil)
    {
        if (!seedsPanel.activeSelf)
        {
            seedsPanel.SetActive(true);
        }
        
        seedsPanel.transform.position = new Vector3(soil.transform.position.x, soil.transform.position.y + 1f);
    }
}