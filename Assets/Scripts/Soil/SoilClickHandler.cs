using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoilClickHandler : MonoBehaviour
{
    [SerializeField] private BuildingManager building;
    [SerializeField] private GameObject seedsPanel;

    [SerializeField] private Seed seedPref;

    private RectTransform seedsPanelRectTransform;
    
    private List<Seed> seeds = new List<Seed>();
    
    public GameObject SeedsPanel { get => seedsPanel; set => seedsPanel = value; }
    
    private void SetUpSeedPanelWidth(int newSize) =>  seedsPanelRectTransform.sizeDelta = new Vector2(newSize, 1);

    private void Start()
    {
        Player.Instance.Inventory.onUpdateInventory += UpdateSeed;
        seedsPanelRectTransform = seedsPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //TODO -- nie działa na telefonie transform.position = Input.mousePosition;

        if (building.isInBuildingMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            CheckFieldsUnderMouse();
        }
    }

    private void UpdateSeed()
    {
        int temp = 0;

        foreach (ItemsData itemsData in Player.Instance.Inventory.CountByItem.Keys)
        {
            if (itemsData is not SeedsData)
            {
                continue;
            }

            if (temp >= seeds.Count)
            {
                Seed seed = Instantiate(seedPref, seedsPanel.transform);
                seed.ClickHandler = this;
                seeds.Add(seed);
            }

            seeds[temp].SeedData = itemsData as SeedsData;
            seeds[temp].gameObject.SetActive(true);
            temp++;
        }

        for (; temp < seeds.Count; temp++)
        {
            seeds[temp].gameObject.SetActive(false);
        }
    }

    private void CheckFieldsUnderMouse()
    {
        if (IsPointerOverUIElement()) return;
        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if (hit.collider && hit.collider.TryGetComponent<Soil>(out var soil))
        {
            if (soil.IsSoilEmpty())
            {
                SeedsPanelHandler(soil);
                return;
            }

            if (soil.IsPlantGrowUp)
            {
                soil.HarvestPlant();
                return;
            }
        }

        seedsPanel.SetActive(false);
    }

    private void SeedsPanelHandler(Soil soil)
    {
        if (!seedsPanel.activeSelf)
        {
            seedsPanel.SetActive(true);
        }

        int temp = 0;
        foreach (var seed in seeds)
        {
            seed.gameObject.SetActive(true);
            if (seed.isUnlocked || Player.Instance.Inventory.CountByItem.ContainsKey(seed.SeedData))
            {
                temp++;
                seed.SetSoil(soil);
            }
            else
            {
                seed.gameObject.SetActive(false);
            }
        }

        SetUpSeedPanelWidth(temp);
        seedsPanel.transform.position = soil.transform.position;
    }
    
    private bool IsPointerOverUIElement()
    {
        if (EventSystem.current == null) return false;

        return EventSystem.current.IsPointerOverGameObject();
    }
}