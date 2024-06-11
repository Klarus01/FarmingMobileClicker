using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buying : MonoBehaviour
{
    [SerializeField] private ItemsData itemSo;
    [SerializeField] private Player player;

    [SerializeField] private int levelRequired;
    [SerializeField] private Image itemImage;
    [SerializeField] private Canvas ShopBagCanvas;

    [SerializeField] private Button button;

    private TextMeshProUGUI priceText;
    private void Awake()
    {
        button.onClick.AddListener(Buy);
        priceText = button.GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = "Level" + levelRequired.ToString();
        player.OnLevelUp += UnlockSlot;
    }

    private void Start()
    {
        UnlockSlot();
    }

    private void UnlockSlot()
    {
        if(player.Level != levelRequired)
        {
            return;
        }

        priceText.text = itemSo.ItemValue.ToString();
        itemImage.sprite = itemSo.ItemSprite;
    }

    private void Buy()
    {
        if(player.Level < levelRequired)
        {
            return;
        }

        if(player.Money < itemSo.ItemValue)
        {
            return;
        }

        if(itemSo is SoilData)
        {
            //Buy soil
            ShopBagCanvas.gameObject.SetActive(false);
            return;
        }

        if (player.UpdateMoney(itemSo.ItemValue * -1))
        {
            player.Inventory.AddItemToInventory(itemSo);
        }
    }
}