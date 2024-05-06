using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Soil : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
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
        growTimeLeft = seedData.TimeToCreate;
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
        var percentage = Player.Instance.CropYield % 1;
        var count = Random.Range(0.0f, 1.0f) <= percentage ? 1 : 0;
        count += Mathf.FloorToInt(Player.Instance.CropYield);
        return count;
    }

    private void GrowingUpPlant()
    {
        if (isPlantGrowUp)
        {
            return;
        }

        growTimeLeft -= (Time.deltaTime * Player.Instance.GrowthSpeed);

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
