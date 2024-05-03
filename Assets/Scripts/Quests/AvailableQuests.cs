using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvailableQuests : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text numberOfItemsNeededText;
    [SerializeField] private Button acceptQuestButton; 
    [SerializeField] private Button rejectQuestButton; 
    private QuestData questData;
    public QuestData QuestData => questData;

    private void Start()
    {
        acceptQuestButton.onClick.AddListener(AcceptQuest);
        rejectQuestButton.onClick.AddListener(RejectQuest);
    }

    private void Update()
    {
        if (questManager.AcceptedQuest.Count >= 3) acceptQuestButton.interactable = false;
        else acceptQuestButton.interactable = true;
    }

    public void NewQuestInit(QuestData newQuestData)
    {
        questData = newQuestData;

        if (!questData) return;
        questNameText.SetText(questData.questName);
        numberOfItemsNeededText.SetText(questData.RequestList[0].plantCount + " " + questData.questName);
    }

    private void AcceptQuest()
    {
        questManager.QuestAccepted(questData);
        questData = null;
        gameObject.SetActive(false);
    }
    
    private void RejectQuest()
    {
        questManager.QuestRejected(questData);
        questData = null;
        gameObject.SetActive(false);
    }
}
