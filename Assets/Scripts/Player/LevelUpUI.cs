using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private GameObject levelUpPanel;

    [SerializeField] private Image unlockedImage;

    [SerializeField] private Button closeButton;

    [SerializeField] private UnlockedItems[] unlockedItems;

    private void Awake()
    {
        player.OnLevelUp += LevelUP;
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void LevelUP()
    {
        levelUpPanel.SetActive(true);
    }

    private void ClosePanel()
    { 
        levelUpPanel.SetActive(false);
        foreach(UnlockedItems unlockedItem in unlockedItems)
        {
            if(unlockedItem.level != player.Level)
            {
                continue;
            }

            unlockedImage.sprite = unlockedItem.unlockedList[0].items.ItemSprite;
        }
    }
}
