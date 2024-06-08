using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private GameObject upgradesMenuPanel;
    [SerializeField] private Button upgradesMenuButton;
    
    [SerializeField] private List<Upgrade> upgradesList = new();
    [SerializeField] private List<UpgradeData> allUpgradesDatasList = new();
    
    public static Action onUpgradeSelected;

    private void Start()
    {
        upgradesMenuButton.onClick.AddListener(GenerateNewUpgrades);
    }
    
    private void OnEnable()
    {
        onUpgradeSelected += OnUpgradeSelected;
        Player.Instance.OnLevelUp += ShowUpgradeMenu;
    }

    private void OnDisable()
    {
        onUpgradeSelected -= OnUpgradeSelected;
        Player.Instance.OnLevelUp += ShowUpgradeMenu;
    }

    private void GenerateNewUpgrades()
    {
        foreach (var upgrade in upgradesList)
        {
            upgrade.gameObject.SetActive(true);
            upgrade.Init((allUpgradesDatasList[Random.Range(0, allUpgradesDatasList.Count)]));
        }
    }

    private void OnUpgradeSelected()
    {
        foreach (var upgrade in upgradesList)
        {
            upgrade.gameObject.SetActive(false);
        }

        Player.Instance.UpgradePointsToSpend -= 1;

        if (Player.Instance.UpgradePointsToSpend.Equals(0))
        {
            upgradesMenuPanel.SetActive(false);
        }
    }

    private void ShowUpgradeMenu()
    {
        upgradesMenuPanel.SetActive(true);
    }
}