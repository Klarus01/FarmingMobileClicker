using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    
    [SerializeField] private Player player;

    [SerializeField] private Canvas inventoryUI;
    [SerializeField] private GameObject bagUI;

    [SerializeField] private Button inventoryButton;

    private void Awake()
    {
        player.OnGrainExp += UpdateExp;
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    private void UpdateExp()
    {
        levelText.text = player.Level.ToString();
        expSlider.value = (float)player.Exp / (float)player.ExpToLevelUp;
        expText.text = player.Exp.ToString() + " / " + player.ExpToLevelUp.ToString();
    }

    private void OpenInventory()
    {
        inventoryUI.gameObject.SetActive(true);
        bagUI.gameObject.SetActive(true);
    }
}
