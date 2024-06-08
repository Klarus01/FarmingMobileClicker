using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Buying : MonoBehaviour
{
    [SerializeField] private SeedsData seedSo;
    [SerializeField] private Player player;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Buy);
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