using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public Action onUpdateInventory;

    private Dictionary<ItemsData, int> countByItem = new();

    public Dictionary<ItemsData, int> CountByItem => countByItem;

    public void AddItemToInventory(ItemsData itemData, int count = 1)
    {
        if(!countByItem.TryAdd(itemData, count))
        {
            countByItem[itemData] += count;
        }

        onUpdateInventory?.Invoke();
    }

    public void ConsumeItem(ItemsData itemsData, int count = 1)
    {
        countByItem[itemsData] -= count;

        if(countByItem[itemsData] == 0)
        {
            countByItem.Remove(itemsData);
        }

        onUpdateInventory?.Invoke();
    }

    public bool CanConsumeItem(ItemsData itemsData)
    {
        return countByItem[itemsData] >= 0;
    }
}
