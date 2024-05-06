using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcceptedQuest : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text numberOfItemsNeededText;
    [SerializeField] private Button doneButton; 
    private QuestData questData;
    private Inventory inventory;

    public QuestData QuestData => questData;

    private void OnEnable()
    {
        Soil.onHarvestPlant += RefreshItemCounts;
    }

    private void OnDisable()
    {
        Soil.onHarvestPlant -= RefreshItemCounts;
    }
    
    private void Start()
    {
        doneButton.onClick.AddListener(DoneQuest);
    }

    public void NewQuestInit(QuestData newQuestData)
    {
        questData = newQuestData;
        if (!questData) return;
        questNameText.SetText(questData.questName);
        RefreshItemCounts();
    }

    private void RefreshItemCounts()
    {
        if (CheckIfQuestIsDone())
        {
            doneButton.interactable = true;
        }
        else
        {
            doneButton.interactable = false;
        }

        bool hasItem = Player.Instance.Inventory.CountByItem.ContainsKey(questData.RequestList[0].plants);
        int itemCount = hasItem ? Player.Instance.Inventory.CountByItem[questData.RequestList[0].plants] : 0;
        numberOfItemsNeededText.SetText(itemCount + "/" + questData.RequestList[0].plantCount);
    }

    private bool CheckIfQuestIsDone()
    {
        if (questData == null) return false;

        foreach (var request in questData.RequestList)
        {
            if (!Player.Instance.Inventory.CountByItem.ContainsKey(request.plants)) return false;
            if (Player.Instance.Inventory.CountByItem[request.plants] < request.plantCount) return false;
        }

        return true;
    }
    
    private void DoneQuest()
    {
        Player.Instance.Inventory.ConsumeItem(questData.RequestList[0].plants, questData.RequestList[0].plantCount);
        Soil.onHarvestPlant?.Invoke();
        questManager.RewardFromQuest(questData);
        questData = null;
        gameObject.SetActive(false);
    }
}