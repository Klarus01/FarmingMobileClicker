using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    [SerializeField] private GameObject panelWithSlots;
    [SerializeField] private TextMeshProUGUI emptyText;
    private Slot[] slots;

    private void Awake()
    {
        slots = panelWithSlots.GetComponentsInChildren<Slot>();
        inventory.onUpdateInventory += UpdateInventory;
    }

    private void Start()
    {
        UpdateInventory();
        Debug.Log(slots.Length);
    }

    private void UpdateInventory()
    {
        int i = 0;

        foreach(ItemsData itemData in inventory.CountByItem.Keys)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Icon.sprite = itemData.ItemSprite;
            slots[i].Count.text = inventory.CountByItem[itemData].ToString();
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
}
