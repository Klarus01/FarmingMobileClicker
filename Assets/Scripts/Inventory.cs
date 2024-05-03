using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemsData, int> countByItem = new Dictionary<ItemsData, int>();

    public Dictionary<ItemsData, int> CountByItem { get { return countByItem; } }

    public void AddItemToInventory(ItemsData itemData, int count = 1)
    {
        if(countByItem.ContainsKey(itemData))
        {
            countByItem[itemData] += count;
        }
        else
        {
            countByItem.Add(itemData, count);
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
