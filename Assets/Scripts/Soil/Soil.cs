using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Soil : MonoBehaviour
{
    [SerializeField] private SpriteRenderer plantSprite;

    private bool isPlantGrowUp;
    private float growTimeLeft;
    private SeedsData seed;

    public bool IsPlantGrowUp => isPlantGrowUp;
    public SeedsData Seed => seed;

    public static Action onHarvestPlant;

    public bool IsSoilEmpty() => seed == null;

    private void Update()
    {
        if (seed == null)
        {
            return;
        }

        GrowingUpPlant();
    }

    public void PlantSeed(SeedsData seedData)
    {
        if (seedData == null)
        {
            return;
        }

        this.seed = seedData;
        isPlantGrowUp = false;
        if (!UpgradeHandler.Instance.ValueByUpgradeType.ContainsKey(UpgradeType.GrowthSpeed)) growTimeLeft = seedData.TimeToCreate;
        else growTimeLeft = seedData.TimeToCreate * (1 - UpgradeHandler.Instance.ValueByUpgradeType[UpgradeType.GrowthSpeed]);
        plantSprite.gameObject.SetActive(true);
        plantSprite.sprite = seedData.ItemSprite;
    }

    public void HarvestPlant()
    {
        Player.Instance.AddExp(seed.Plant.ExpFromCollect);
        Player.Instance.Inventory.AddItemToInventory(seed.Plant, NumberOfPlantsToHarvest());
        onHarvestPlant?.Invoke();
        seed = null;
        plantSprite.gameObject.SetActive(false);
    }

    private int NumberOfPlantsToHarvest()
    {
        var count = 0;
        if (!UpgradeHandler.Instance.ValueByUpgradeType.ContainsKey(UpgradeType.CropYield)) count = 0;
        else
        {
            var percentage = UpgradeHandler.Instance.ValueByUpgradeType[UpgradeType.CropYield] % 1;
            count = Random.Range(0.0f, 1.0f) <= percentage ? 1 : 0;
            count += Mathf.FloorToInt(UpgradeHandler.Instance.ValueByUpgradeType[UpgradeType.CropYield]);
        }
        return count + 1;
    }

    private void GrowingUpPlant()
    {
        if (isPlantGrowUp)
        {
            return;
        }

        growTimeLeft -= Time.deltaTime;

        if (growTimeLeft <= 0)
        {
            GrowUpPlant();
            return;
        }

        SetPlantSprite();
    }

    private void GrowUpPlant()
    {
        plantSprite.sprite = seed.Plant.ItemSprite;
        isPlantGrowUp = true;
    }

    private void SetPlantSprite()
    {
        int partOfSprite = seed.PlantStadiumSprites.Count;

        float timeToChangeSprite = seed.TimeToCreate / partOfSprite;

        int numberOfSprite = Mathf.FloorToInt((seed.TimeToCreate - growTimeLeft) / timeToChangeSprite);

        if (numberOfSprite >= partOfSprite)
        {
            numberOfSprite = partOfSprite - 1;
        }

        plantSprite.sprite = seed.PlantStadiumSprites[numberOfSprite];
    }
}
