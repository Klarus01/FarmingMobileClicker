using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Selling : MonoBehaviour
{
    [SerializeField] private SeedsData plantSo;
    [SerializeField] private Player player;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Sell);
    }

    private void Sell()
    {
        if (player.Inventory.CanConsumeItem(plantSo))
        {
            player.Inventory.ConsumeItem(plantSo);
            player.UpdateMoney(plantSo.ItemValue);
        }
        
        Debug.Log(player.Money);
        Debug.Log(player.Inventory.CountByItem);
    }
}