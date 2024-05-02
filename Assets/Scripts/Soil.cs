using System;
using UnityEngine;

public class Soil : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private SpriteRenderer plantSprite;
    
    private bool isPlantGrowUp;
    private float growTimeLeft;
    private SeedsData seed;

    public bool IsPlantGrowUp => isPlantGrowUp;
    public SeedsData Seed => seed;


    public bool IsSoilEmpty() => seed == null;
    
    private void Update()
    {
        if (seed == null)
        {
            return;
        }

        GrowingUpPlant();
    }

    public void PlantSeed(SeedsData seed)
    {
        if (seed == null)
        {
            return;
        }

        this.seed = seed;
        isPlantGrowUp = false;
        growTimeLeft = seed.TimeToCreate;
        plantSprite.gameObject.SetActive(true);
        plantSprite.sprite = seed.ItemSprite;
    }

    public void HarvestPlant()
    {
        seed = null;
        plantSprite.gameObject.SetActive(false);
        //inventory.AddItemToInventory(seed.Plant);
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
