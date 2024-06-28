using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject InventoryPanel;

    [SerializeField] private GameObject panelWithSlots;
    [SerializeField] private TextMeshProUGUI emptyText;

    [SerializeField] private ItemCountManager itemCountManager;

    private Slot[] slots;

    private float sellCooldown = 1;

    private void Awake()
    {
        itemCountManager.onChangeCount += UpdateInventory;
        slots = panelWithSlots.GetComponentsInChildren<Slot>();
        inventory.onUpdateInventory += UpdateInventory;
    }

    private void OnEnable()
    {
        UpdateInventory();
        ShopPanel.SetActive(false);
    }

    private void OnDisable()
    {
        InventoryPanel.SetActive(false);
    }

    private void Start()
    {
        UpdateInventory();
    }

    private void Update()
    {
        sellCooldown -= Time.deltaTime;
    }

    private void UpdateInventory()
    {
        int i = 0;

        foreach(ItemsData itemData in inventory.CountByItem.Keys)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Icon.sprite = itemData.ItemSprite;
            slots[i].Count.text = inventory.CountByItem[itemData].ToString();
            slots[i].SellButton.onClick.RemoveAllListeners();
            if(itemCountManager.isMaxCount)
            {
                slots[i].SellButton.onClick.AddListener(delegate { SellItem(itemData, inventory.CountByItem[itemData]); });
                slots[i].PriceText.text = ((itemData.ItemValue / 2) * inventory.CountByItem[itemData] + " $");
            }
            else
            {
                slots[i].SellButton.onClick.AddListener(delegate { SellItem(itemData, itemCountManager.sellCount); });
                slots[i].PriceText.text = ((itemData.ItemValue / 2) * itemCountManager.sellCount + " $");
            }

            i++;
        }

        for(;i <slots.Length;i++)
        {
            slots[i].gameObject.SetActive(false);
        }

        emptyText.gameObject.SetActive(true);

        foreach (Slot slot in slots)
        {
            if(!slot.gameObject.activeSelf)
            {
                continue;
            }

            emptyText.gameObject.SetActive(false);
        }

    }

    private void SellItem(ItemsData itemsData, int count)
    {
        if(sellCooldown > 0)
        {
            return;
        }

        if(count > inventory.CountByItem[itemsData])
        {
            Player.Instance.UpdateMoney((itemsData.ItemValue / 2) * inventory.CountByItem[itemsData]);
            inventory.ConsumeItem(itemsData, inventory.CountByItem[itemsData]);
        }
        else
        {
            Player.Instance.UpdateMoney((itemsData.ItemValue / 2) * count);
            inventory.ConsumeItem(itemsData, count);
        }

        sellCooldown = 1;
        
        UpdateInventory();
    }
}
