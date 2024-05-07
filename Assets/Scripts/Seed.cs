using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    [SerializeField] private SoilClickHandler clickHandler;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private SeedsData seedData;
    private Soil soil;
    public bool isUnlocked;
    public SeedsData SeedData => seedData;
    
    private void Start()
    {
        image.sprite = seedData.ItemSprite;
        button.onClick.AddListener(PlantSeedInSoil);
    }

    public void SetSoil(Soil newSoil)
    {
        this.soil = newSoil;
    }

    private void PlantSeedInSoil()
    {
        soil.PlantSeed(seedData);
        clickHandler.SeedsPanel.SetActive(false);
    }
}