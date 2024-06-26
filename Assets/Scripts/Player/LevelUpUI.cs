using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel;

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Image unlockedImage;

    [SerializeField] private Button closeButton;

    [SerializeField] private UnlockedItems[] unlockedItems;

    private void Awake()
    {
        Player.Instance.OnLevelUp += LevelUP;
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void LevelUP()
    {
        levelUpPanel.SetActive(true);

        foreach (UnlockedItems unlockedItem in unlockedItems)
        {
            if (unlockedItem.level != Player.Instance.Level)
            {
                continue;
            }
            Debug.Log("XD");
            levelText.text = Player.Instance.Level.ToString();
            unlockedImage.sprite = unlockedItem.unlockedList[0].items.ItemSprite;
        }
    }

    private void ClosePanel()
    { 
        levelUpPanel.SetActive(false);
    }
}
