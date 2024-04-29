using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoilClickHandler : MonoBehaviour
{
    [SerializeField] private BuildingManager building;
    [SerializeField] private Seed[] seeds;
    [SerializeField] private GameObject seedsPanel;
    
    public GameObject SeedsPanel { get => seedsPanel; set => seedsPanel = value; }

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
        if (RectTransformUtility.RectangleContainsScreenPoint(seedsPanel.GetComponent<RectTransform>(),Input.mousePosition, Camera.main)) return;
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.TryGetComponent<Soil>(out var soil))
        {
            if (soil.IsSoilEmpty())
            {
                SeedsPanelHandler(soil);
                return;
            }
            else
            {
                if (soil.IsPlantGrowUp)
                {
                    soil.HarvestPlant();
                }
            }
        }

        seedsPanel.SetActive(false);
    }

    private void SeedsPanelHandler(Soil soil)
    {
        if (!seedsPanel.activeSelf)
        {
            seedsPanel.SetActive(true);
        }

        foreach (var seed in seeds)
        {
            seed.SetSoil(soil);
        }
        
        seedsPanel.transform.position = new Vector3(soil.transform.position.x, soil.transform.position.y + 1f);
    }
}