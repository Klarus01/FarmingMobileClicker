using UnityEngine;
using UnityEngine.UI;

public class NewBuilding : MonoBehaviour
{
    [SerializeField] private Button buySoilButton;
    [SerializeField] private Button buyBuildingButton;
    [SerializeField] private Soil soliPrefab;
    [SerializeField] private Building buildingPrefab;

    private void Start()
    {
        buySoilButton.onClick.AddListener(BuyNewSoil);
        buyBuildingButton.onClick.AddListener(BuyNewBuilding);
    }

    private void BuyNewSoil()
    {
        Instantiate(soliPrefab, transform.position, Quaternion.identity);
    }

    private void BuyNewBuilding()
    {
        Instantiate(buildingPrefab, transform.position, Quaternion.identity);
    }
}