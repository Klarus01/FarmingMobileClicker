using UnityEngine;
using UnityEngine.EventSystems;

public class SoilClickHandler : MonoBehaviour
{
    [SerializeField] private BuildingManager building;
    [SerializeField] private Seed[] seeds;
    [SerializeField] private GameObject seedsPanel;
    private RectTransform seedsPanelRectTransform;
    private int seedPanelWidth;
    
    public GameObject SeedsPanel { get => seedsPanel; set => seedsPanel = value; }
    
    private void SetUpSeedPanelWidth(int newSize) =>  seedsPanelRectTransform.sizeDelta = new Vector2(newSize, 1);

    private void Start()
    {
        seedsPanelRectTransform = seedsPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;

        if (building.isInBuildingMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            CheckFieldsUnderMouse();
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
        seedsPanel.transform.position = new Vector3(soil.transform.position.x, soil.transform.position.y + 1f);
    }
    
    private bool IsPointerOverUIElement()
    {
        if (EventSystem.current == null) return false;

        return EventSystem.current.IsPointerOverGameObject();
    }
}