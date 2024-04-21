using UnityEngine;

public class Soil : MonoBehaviour
{
    [SerializeField] private SpriteRenderer plantSprite;

    private bool isPlantGrowUp;
    private float growTimeLeft;
    private Seeds seed;
    private Inventory inventory;

    private void Update()
    {
        if (seed == null)
        {
            return;
        }

        GrowingUpPlant();
    }

    public void PlantSeed(Seeds seed)
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

    private void HarvestPlant()
    {
        seed = null;
        plantSprite.gameObject.SetActive(false);
        inventory.AddItemToInventory(seed.Plant);
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

        int numberOfSprite = (int)(seed.TimeToCreate / timeToChangeSprite);

        if (numberOfSprite == 0)
        {
            return;
        }

        plantSprite.sprite = seed.PlantStadiumSprites[numberOfSprite - 1];
    }
}