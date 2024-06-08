using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Button buySoilButton;
    [SerializeField] private Button buyBuildingButton;
    [SerializeField] private Soil soliPrefab;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private BuildingPlacingHandler placingHandler;
    public bool isInBuildingMode = false;
    
    private void Start()
    {
        buySoilButton.onClick.AddListener(BuyNewSoil);
        buyBuildingButton.onClick.AddListener(BuyNewBuilding);
    }

    private void Update()
    {
        if(isInBuildingMode) CheckIfStillInBuildingMode();
    }

    private void BuyNewSoil()
    {
        Soil newSoil = Instantiate(soliPrefab, transform);
        placingHandler = newSoil.GetComponent<BuildingPlacingHandler>();
        isInBuildingMode = true;
    }

    private void BuyNewBuilding()
    {
        Building newBuilding = Instantiate(buildingPrefab, transform);
        placingHandler = newBuilding.GetComponent<BuildingPlacingHandler>();
        isInBuildingMode = true;
    }

    private void CheckIfStillInBuildingMode() 
    {
        if (placingHandler == null)
        {
            isInBuildingMode = false;
        }
    }
}