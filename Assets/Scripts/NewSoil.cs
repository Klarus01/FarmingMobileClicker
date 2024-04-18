using UnityEngine;
using UnityEngine.UI;

public class NewSoil : MonoBehaviour
{
    [SerializeField] private Button buySoilButton;
    [SerializeField] private Soil soliPrefab;

    private void Start()
    {
        buySoilButton.onClick.AddListener(BuyNewSoil);
    }

    private void BuyNewSoil()
    {
        Instantiate(soliPrefab);
    }
}