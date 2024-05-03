using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemsData, int> countByItem = new();

    public Dictionary<ItemsData, int> CountByItem => countByItem;

    public void AddItemToInventory(ItemsData itemData, int count = 1)
    {
        if(!countByItem.TryAdd(itemData, count))
        {
            countByItem[itemData] += count;
        }
    }

    public void ConsumeItem(ItemsData itemsData, int count = 1)
    {
        countByItem[itemsData] -= count;

        if(countByItem[itemsData] == 0)
        {
            countByItem.Remove(itemsData);
        }
    }
}
