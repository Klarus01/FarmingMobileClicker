using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buying : MonoBehaviour
{
    [SerializeField] private ItemsData itemSo;

    [SerializeField] private int levelRequired;
    [SerializeField] private Image itemImage;
    [SerializeField] private Canvas ShopBagCanvas;

    [SerializeField] private Button button;
    [SerializeField] private ItemCountManager itemCountManager;

    private TextMeshProUGUI priceText;
    private void Awake()
    {
        itemCountManager.onChangeCount += UpdateShop;

        button.onClick.AddListener(Buy);
        priceText = button.GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = "Level" + levelRequired.ToString();
        Player.Instance.OnLevelUp += UnlockSlot;
    }

    private void Start()
    {
        UnlockSlot();
    }

    private void UpdateShop()
    {
        if (Player.Instance.Level != levelRequired)
        {
            return;
        }

        priceText.text = itemSo.ItemValue * itemCountManager.sellCount + " $";
    }

    private void UnlockSlot()
    {
        itemImage.sprite = itemSo.ItemSprite;
        if(Player.Instance.Level != levelRequired)
        {
            return;
        }

        priceText.text = itemSo.ItemValue.ToString();
    }

    private void Buy()
    {
        if(Player.Instance.Level < levelRequired)
        {
            return;
        }

        if(Player.Instance.Money * itemCountManager.sellCount < itemSo.ItemValue)
        {
            return;
        }

        if(itemSo is SoilData)
        {
            //Buy soil
            ShopBagCanvas.gameObject.SetActive(false);
            return;
        }

        if (Player.Instance.UpdateMoney(itemSo.ItemValue * -1 * itemCountManager.sellCount))
        {
            Player.Instance.Inventory.AddItemToInventory(itemSo, itemCountManager.sellCount);
        }
    }
}