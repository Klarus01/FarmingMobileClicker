using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string Name { get; private set; }
    public Dictionary<ItemsData, int> RequiredItems { get; private set; }
    public ItemsData Product { get; private set; }
    public int ProductionTime { get; private set; }

    public Building(string name, Dictionary<ItemsData, int> requiredItems, ItemsData product, int productionTime)
    {
        Name = name;
        RequiredItems = requiredItems;
        Product = product;
        ProductionTime = productionTime;
    }

    public bool CanProduce(Inventory inventory)
    {
        foreach (var requiredItem in RequiredItems)
        {
            if (!inventory.CountByItem.ContainsKey(requiredItem.Key) || inventory.CountByItem[requiredItem.Key] < requiredItem.Value)
            {
                return false;
            }
        }
        return true;
    }

    public void Produce(Inventory inventory)
    {
        if (CanProduce(inventory))
        {
            foreach (var requiredItem in RequiredItems)
            {
                inventory.ConsumeItem(requiredItem.Key, requiredItem.Value);
            }

            StartCoroutine(StartProduction(inventory));
        }
        else
        {
            Debug.Log("Nie masz wystarczającej ilości plonów!");
        }
    }

    private IEnumerator StartProduction(Inventory inventory)
    {
        yield return new WaitForSeconds(ProductionTime);
        inventory.AddItemToInventory(Product, 1);
        Debug.Log("End of the production!");
    }
}