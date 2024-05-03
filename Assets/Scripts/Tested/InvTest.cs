using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvTest : MonoBehaviour
{
    [SerializeField] private ItemsData _items;

    private void Start()
    {
        Player.Instance.Inventory.AddItemToInventory(_items, 5);

        Debug.Log(Player.Instance.Inventory.CountByItem[_items]);

        Player.Instance.Inventory.ConsumeItem(_items);

        Debug.Log(Player.Instance.Inventory.CountByItem[_items]);

    }
}
