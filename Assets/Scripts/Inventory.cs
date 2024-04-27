using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemsData, int> countByItem = new Dictionary<ItemsData, int>();

    public void AddItemToInventory(ItemsData items, int count = 1)
    {
        if(countByItem.ContainsKey(items))
        {
            countByItem[items] += count;
        }
        else
        {
            countByItem.Add(items, count);
        }
    }
}
