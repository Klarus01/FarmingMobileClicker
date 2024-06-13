using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ClickableObject : MonoBehaviour
{
    public enum ObjectType { Field, Animal, Building }
    public ObjectType objectType;

    [System.Serializable]
    public class Action
    {
        public string actionName;
        public string displayName;
        public ItemsData neededItems;
        public ItemsData newItem;
        public int neededItemsCount;
        public float productionTime;
    }

    public List<Action> actions = new List<Action>();

    public GameObject actionPanel;
    public Button actionButtonPrefab;
    private List<Button> instantiatedButtons = new List<Button>();

    void Start()
    {
        actionPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        ShowActions();
    }

    void ShowActions()
    {
        foreach (Button button in instantiatedButtons)
        {
            Destroy(button.gameObject);
        }
        instantiatedButtons.Clear();

        foreach (var action in actions)
        {
            Button newButton = Instantiate(actionButtonPrefab, actionPanel.transform);
            newButton.name = action.actionName;
            newButton.onClick.AddListener(() => TryPerformAction(action.actionName, action.neededItems, action.neededItemsCount, action.productionTime, action.newItem));
            instantiatedButtons.Add(newButton);
        }

        actionPanel.SetActive(true);
    }

    void TryPerformAction(string actionName, ItemsData neededItems, int neededItemsCount, float productionTime, ItemsData newItem)
    {
        if (!Player.Instance.Inventory.CountByItem.TryGetValue(neededItems, out var value))
        {
            return;
        }
        if(neededItemsCount > value)
        {
            return;
        }
        
        Player.Instance.Inventory.ConsumeItem(neededItems, neededItemsCount);
        StartCoroutine(StartProduction(productionTime, newItem));
        actionPanel.SetActive(false);
    }
    
    private IEnumerator StartProduction(float productionTime, ItemsData newItem)
    {
        yield return new WaitForSeconds(productionTime);
        Player.Instance.Inventory.AddItemToInventory(newItem);
        Debug.Log("End of the production!");
    }
}
