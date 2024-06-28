using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using TMPro;

public class Soil : MonoBehaviour
{
    [SerializeField] private SpriteRenderer plantSprite;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool isPlantGrowUp;
    private float growTimeLeft;
    private SeedsData seed;

    public bool IsPlantGrowUp { get => isPlantGrowUp; set => isPlantGrowUp = value; }
    public SeedsData Seed { get => seed; set => isPlantGrowUp = seed; }

    public static Action onHarvestPlant;

    public bool IsSoilEmpty() => seed == null;

    private void Update()
    {
        if (seed == null)
        {
            return;
        }

        //timerText.transform.position = transform.position;
        timerText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        UpdateTimer();
        GrowingUpPlant();
        ShowTimer();
    }

    public void PlantSeed(SeedsData seedData)
    {
        if (seedData == null)
        {
            return;
        }

        this.seed = seedData;
        Player.Instance.Inventory.ConsumeItem(seedData);
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
        isPlantGrowUp = false;
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

    private void UpdateTimer()
    {
        float tmpTime = growTimeLeft;

        int hour = (int)tmpTime / 3600;

        tmpTime -= hour * 3600;

        int min = (int)tmpTime / 60;

        tmpTime -= min * 60;

        int sec = (int)tmpTime;

        timerText.text = hour + ":" + min + ":" + sec;
    }

    private void ShowTimer()
    {
        if(seed == null)
        {
            timerText.gameObject.SetActive(false);
            return;
        }

        if(growTimeLeft <= 0)
        {
            timerText.gameObject.SetActive(false);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                var soil = hit.collider.GetComponent<Soil>();

                if (soil == this)
                {
                    timerText.gameObject.SetActive(true);
                    return;
                }
            }

            timerText.gameObject.SetActive(false);
        }
    }
}
