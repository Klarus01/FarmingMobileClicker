using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<Items, int> countByItem = new Dictionary<Items, int>();

    public void AddItemToInventory(Items items, int count = 1)
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
