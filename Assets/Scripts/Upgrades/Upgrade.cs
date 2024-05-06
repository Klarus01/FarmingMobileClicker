using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeValueText;
    [SerializeField] private Button upgradeSelectionButton;
    public UpgradeData upgradeData;

    private void Start()
    {
        upgradeSelectionButton.onClick.AddListener(UpgradeSelected);
    }

    public void Init(UpgradeData newUpgradeData)
    {
        upgradeData = newUpgradeData;
        if (!upgradeData) return;
        upgradeNameText.SetText(upgradeData.upgradeName);
        upgradeValueText.SetText(upgradeData.value.ToString());
    }

    private void UpgradeSelected()
    {
        upgradeData.ApplyUpgrade();
        UpgradeManager.onUpgradeSelected?.Invoke();
    }
}
