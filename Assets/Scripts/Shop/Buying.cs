using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buying : MonoBehaviour
{
    [SerializeField] private SeedsData seedSo;
    [SerializeField] private Player player;

    private Button button;
    private TextMeshProUGUI priceText;
    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Buy);
        priceText = button.GetComponentInChildren<TextMeshProUGUI>();
        priceText.text = seedSo.ItemValue.ToString();
    }

    private void Buy()
    {
        if (player.UpdateMoney(seedSo.ItemValue * -1))
        {
            player.Inventory.AddItemToInventory(seedSo);
        }
        
        Debug.Log(player.Money);
        Debug.Log(player.Inventory.CountByItem);
    }
}