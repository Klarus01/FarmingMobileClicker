using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel;

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Image unlockedImage;

    [SerializeField] private Button closeButton;

    [SerializeField] private Image upgradeImage;

    [SerializeField] private TextMeshProUGUI upgradeText;

    [SerializeField] private UnlockedItems[] unlockedItems;

    [SerializeField] private List<UpgradeData> upgradeDatas;

    private void Awake()
    {
        Player.Instance.OnLevelUp += LevelUP;
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void LevelUP()
    {
        int randomIndex = Random.Range(0, upgradeDatas.Count);

        upgradeDatas[randomIndex].ApplyUpgrade();

        upgradeImage.color = upgradeDatas[randomIndex].color;

        upgradeText.text = upgradeDatas[randomIndex].type + " +" + upgradeDatas[randomIndex].value.ToString();

        levelUpPanel.SetActive(true);

        foreach (UnlockedItems unlockedItem in unlockedItems)
        {
            if (unlockedItem.level != Player.Instance.Level)
            {
                continue;
            }
            levelText.text = Player.Instance.Level.ToString();
            unlockedImage.sprite = unlockedItem.unlockedList[0].items.ItemSprite;
        }
    }

    private void ClosePanel()
    { 
        levelUpPanel.SetActive(false);
    }
}
