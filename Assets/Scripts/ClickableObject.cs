using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableObject : MonoBehaviour
{
    private enum ObjectType { Field, Animal, Building }
    [SerializeField] private ObjectType objectType;

    [System.Serializable]
    public class Action
    {
        public string actionName;
        public ItemsData neededItems;
        public ItemsData newItem;
        public int neededItemsCount;
        public float productionTime;
    }

    public List<Action> actions = new();

    [SerializeField] private GameObject actionPanel;
    [SerializeField] private Button actionButtonPrefab;
    [SerializeField] private Button fullscreenButton;

    private List<Button> instantiatedButtons = new();
    private bool ignoreNextClick;

    private static ClickableObject currentlyActiveObject;

    void Start()
    {
        actionPanel.SetActive(false);
        fullscreenButton.gameObject.SetActive(false);

        fullscreenButton.onClick.AddListener(CloseActionPanel);
    }

    void OnMouseDown()
    {
        if (currentlyActiveObject != null && currentlyActiveObject != this)
        {
            currentlyActiveObject.CloseActionPanel();
        }

        currentlyActiveObject = this;
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
            bool hasItem = Player.Instance.Inventory.CountByItem.ContainsKey(action.neededItems);
            int itemCount = hasItem ? Player.Instance.Inventory.CountByItem[action.neededItems] : 0;
            newButton.GetComponentInChildren<TMP_Text>().SetText($"{itemCount}/{action.neededItemsCount} turnip\nyou will produce: {action.newItem.name}");
            newButton.name = action.actionName;
            newButton.onClick.AddListener(() => TryPerformAction(action.actionName, action.neededItems, action.neededItemsCount, action.productionTime, action.newItem));
            instantiatedButtons.Add(newButton);
        }

        actionPanel.SetActive(true);
        fullscreenButton.gameObject.SetActive(true);
        ignoreNextClick = true;
    }

    void CloseActionPanel()
    {
        if (ignoreNextClick)
        {
            ignoreNextClick = false;
            return;
        }

        actionPanel.SetActive(false);
        fullscreenButton.gameObject.SetActive(false);
        if (currentlyActiveObject == this)
        {
            currentlyActiveObject = null;
        }
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
        CloseActionPanel();
    }

    private IEnumerator StartProduction(float productionTime, ItemsData newItem)
    {
        yield return new WaitForSeconds(productionTime);
        Player.Instance.Inventory.AddItemToInventory(newItem);
        Debug.Log("End of the production!");
    }
}