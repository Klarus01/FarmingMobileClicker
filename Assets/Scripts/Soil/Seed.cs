using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    [SerializeField] private SoilClickHandler clickHandler;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    
    private SeedsData seedData;
    private Inventory inventory;
    private Soil soil;
    public bool isUnlocked;
    public SeedsData SeedData { get { return seedData; } set { seedData = value; } }
    public SoilClickHandler ClickHandler { get { return clickHandler; } set {  clickHandler = value; } }
    
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
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
        inventory.ConsumeItem(seedData);
        clickHandler.SeedsPanel.SetActive(false);
    }
}