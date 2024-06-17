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

    [SerializeField] private Canvas inventoryUI;
    [SerializeField] private GameObject bagUI;

    [SerializeField] private Button inventoryButton;

    private void Awake()
    {
        Player.Instance.OnGrainExp += UpdateExp;
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    private void UpdateExp()
    {
        levelText.text = Player.Instance.Level.ToString();
        expSlider.value = (float)Player.Instance.Exp / (float)Player.Instance.ExpToLevelUp;
        expText.text = Player.Instance.Exp.ToString() + " / " + Player.Instance.ExpToLevelUp.ToString();
    }

    private void OpenInventory()
    {
        inventoryUI.gameObject.SetActive(true);
        bagUI.gameObject.SetActive(true);
    }
}
